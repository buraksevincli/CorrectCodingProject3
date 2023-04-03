using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Abstracts.StateMachines;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.StateMachines.EnemyStates
{
    public class Idle : IState
    {
        private IMover _mover;
        private IMyAnimation _animation;

        private float _maxStandTime;
        private float _currentStandTime;
        
        public bool IsIdle { get; private set; }
        
        public Idle(IMover mover, IMyAnimation animation)
        {
            _mover = mover;
            _animation = animation;
        }
        
        public void OnEnter()
        {
            IsIdle = true;
            _animation.MoveAnimation(0f);

            _maxStandTime = Random.Range(4f, 10f);
        }

        public void OnExit()
        {
            _currentStandTime = 0f;

        }
        
        public void Tick()
        {
            _mover.Tick(0f);

            _currentStandTime += Time.deltaTime;

            if (_currentStandTime > _maxStandTime)
            {
                IsIdle = false;
            }
        }
    }
}

