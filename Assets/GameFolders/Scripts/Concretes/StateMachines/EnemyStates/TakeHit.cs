using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Combats;
using GameFolders.Scripts.Abstracts.StateMachines;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.StateMachines.EnemyStates
{
    public class TakeHit : IState
    {
        private IMyAnimation _animation;

        private float _maxDelayTime = 0.3f;
        private float _currentDelayTime = 0f;
        
        public bool IsTakeHit { get; private set; }

        public TakeHit(IHealth health, IMyAnimation animation)
        {
            _animation = animation;
            health.OnHealthChanged += (currentHealth,maxHealth) => OnEnter();
        }
        
        public void OnEnter()
        {
            IsTakeHit = true;
        }

        public void OnExit()
        {
            _currentDelayTime = 0f;
        }
        
        public void Tick()
        {
            _currentDelayTime += Time.deltaTime;
            
            if (_currentDelayTime > _maxDelayTime && IsTakeHit)
            {
                _animation.TakeHitAnimation();
                IsTakeHit = false;
            }
        }
    }
}
