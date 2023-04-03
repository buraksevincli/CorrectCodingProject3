using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Abstracts.StateMachines;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.StateMachines.EnemyStates
{
    public class Attack : IState
    {
        private IMyAnimation _animation;
        private IAttacker _attacker;
        private IHealth _playerHealth;
        private IFlip _flip;
        private System.Func<bool> _isPlayerRightSide;
        

        private float _currentAttackTime;
        private float _maxAttackTime;

        public Attack(IHealth playerHealth, IFlip flip, IMyAnimation animation, IAttacker attacker, float maxAttackTime, System.Func<bool> isPlayerRightSide)
        {
            _flip = flip;
            _animation = animation;
            _attacker = attacker;
            _playerHealth = playerHealth;
            _maxAttackTime = maxAttackTime;
            _isPlayerRightSide = isPlayerRightSide;
        }
        
        public void OnEnter()
        {
            _currentAttackTime = 0f;
        }

        public void OnExit()
        {
        }
        public void Tick()
        {
            _currentAttackTime += Time.deltaTime;

            if (_currentAttackTime >  _maxAttackTime)
            {
                _flip.FlipCharacter(_isPlayerRightSide.Invoke() ? 1f : -1f);
                _animation.AttackAnimation();
                _attacker.Attack(_playerHealth);
                _currentAttackTime = 0f;
            }
        }
        
    }
}
