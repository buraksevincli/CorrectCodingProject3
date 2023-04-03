using System.Collections;
using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Abstracts.Controllers;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Concretes.Animations;
using GameFolders.Scripts.Concretes.Movements;
using GameFolders.Scripts.Concretes.StateMachines;
using GameFolders.Scripts.Concretes.StateMachines.EnemyStates;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class EnemyController : MonoBehaviour, IEntityController
    {
        [Header("Movements")]
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private Transform[] patrols;
        
        [Header("Attacks")]
        [SerializeField] private float chaseDistance = 3f;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private float maxAttackTime = 2f;

        [Header("Scores")]
        [SerializeField] private ScoreController scorePrefab;
        [SerializeField] private int currentChance;
        [SerializeField] private int maxChance = 70;
        [SerializeField] private int minChance = 20;
        
        private StateMachine _stateMachine;
        private IEntityController _player;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _player = FindObjectOfType<PlayerController>();
        }

        private IEnumerator Start()
        {
            currentChance = Random.Range(minChance, maxChance);
            IMover mover = new MoverWithTranslate(this, moveSpeed);
            IMyAnimation animation = new CharacterAnimation(GetComponent<Animator>());
            IFlip flip = new FlipWithTransform(this);
            IHealth health = GetComponent<IHealth>();
            IAttacker attacker = GetComponent<IAttacker>();
            IStopEdge stopEdge = GetComponent<IStopEdge>();
            
            Idle idle = new Idle(mover, animation);
            Walk walk = new Walk(this, mover, animation, flip, patrols);
            ChasePlayer chasePlayer = new ChasePlayer(mover, flip, animation, stopEdge, IsPlayerRightSide);
            Attack attack = new Attack(_player.transform.GetComponent<IHealth>(), flip, animation, attacker, maxAttackTime, IsPlayerRightSide);
            TakeHit takeHit = new TakeHit(health, animation);
            Dead dead = new Dead(this, animation, () =>
            {
                if (currentChance > Random.Range(0,100))
                {
                    Instantiate(scorePrefab, transform.position, Quaternion.identity);
                }
                
            });

            _stateMachine.AddTransition(idle, walk, () => !idle.IsIdle);
            _stateMachine.AddTransition(idle, chasePlayer, () => DistanceFromMeToPlayer() < chaseDistance);
            _stateMachine.AddTransition(walk, chasePlayer, () => DistanceFromMeToPlayer() < chaseDistance);
            _stateMachine.AddTransition(chasePlayer, attack, () => DistanceFromMeToPlayer() < attackDistance);

            _stateMachine.AddTransition(walk, idle, () => !walk.IsWalking);
            _stateMachine.AddTransition(chasePlayer, idle, () => DistanceFromMeToPlayer() > chaseDistance);
            _stateMachine.AddTransition(chasePlayer, walk, () => DistanceFromMeToPlayer() > chaseDistance);
            _stateMachine.AddTransition(attack, chasePlayer, () => DistanceFromMeToPlayer() > attackDistance);

            _stateMachine.AddAnyState(dead, () => health.IsDead);
            _stateMachine.AddAnyState(takeHit, () => takeHit.IsTakeHit);

            _stateMachine.AddTransition(takeHit, chasePlayer, () => takeHit.IsTakeHit == false);
            
            _stateMachine.SetState(idle);
            
            yield return null;
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }

        private void OnDrawGizmos()
        {
            OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private float DistanceFromMeToPlayer()
        {
            return Vector2.Distance(transform.position, _player.transform.position);
        }

        private bool IsPlayerRightSide()
        {
            Vector3 result = _player.transform.position - this.transform.position;

            if (result.x > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}