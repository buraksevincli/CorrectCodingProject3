using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Controllers;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Abstracts.StateMachines;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.StateMachines.EnemyStates
{
    public class Walk : IState
    {
        private IMover _mover;
        private IMyAnimation _animation;
        private IEntityController _entityController;
        private IFlip _flip;

        private int _patroLIndex = 0;
        private float _direction;
        private Transform[] _patrols;
        private Transform _currentPatrol;

        public bool IsWalking { get; private set; }

        public Walk(IEntityController entityController, IMover mover, IMyAnimation animation, IFlip flip, params Transform[] patrols)
        {
            _mover = mover;
            _animation = animation;
            _entityController = entityController;
            _patrols = patrols;
            _flip = flip;
        }
        
        public void OnEnter()
        {
            if (_patrols.Length < 1) return;
            
            _currentPatrol = _patrols[_patroLIndex];
            
            Vector3 leftOrRight = _currentPatrol.position - _entityController.transform.position;
            if (leftOrRight.x > 0f)
            {
                _flip.FlipCharacter(1f);
            }
            else
            {
                _flip.FlipCharacter(-1f);
            }
            
            _direction = _entityController.transform.localScale.x;
            
            _animation.MoveAnimation(1f);
            
            IsWalking = true;
        }

        public void OnExit()
        {
            _animation.MoveAnimation(0f);

            _patroLIndex++;

            if (_patroLIndex >= _patrols.Length)
            {
                _patroLIndex = 0;
            }
        }
        
        public void Tick()
        {
            if(_currentPatrol == null) return;
            
            if (Vector2.Distance(_entityController.transform.position,_currentPatrol.position) <= 0.2f)
            {
                IsWalking = false;
                return;
            }
            
            _mover.Tick(_direction);
        }
    }
}
