using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Concretes.Animations;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Combats
{
    public class PlayerAttacker : Attacker
    {
        [SerializeField] private Transform attackDirection;
        [SerializeField] private float attackRadius = 1f;

        private Collider2D[] _attackResults;

        private void Awake()
        {
            _attackResults = new Collider2D[10];
        }

        private void OnEnable()
        {
            GetComponent<AnimationImpactWatcher>().OnImpact += HandleImpact;
        }

        private void OnDisable()
        {
            GetComponent<AnimationImpactWatcher>().OnImpact -= HandleImpact;
        }
        
        private void HandleImpact()
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(attackDirection.position + attackDirection.forward,
                attackRadius, _attackResults);

            for (int i = 0; i < hitCount; i++)
            {
                ITakeHit takeHit = _attackResults[i].GetComponent<ITakeHit>();

                if (takeHit != null)
                {
                    Attack(takeHit);
                }
            }
        }

        private void OnDrawGizmos()
        {
            OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackDirection.position+attackDirection.forward,attackRadius);
        }
    }
}

