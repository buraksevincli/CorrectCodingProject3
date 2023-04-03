using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Controllers;
using GameFolders.Scripts.Abstracts.StateMachines;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.StateMachines.EnemyStates
{
    public class Dead : IState
    {
        private IMyAnimation _animation;
        private IEntityController _controller;
        private System.Action _deadCallBack;

        private float _currentTime = 0f;
        
        public Dead(IEntityController controller,IMyAnimation animation, System.Action deadCallBack)
        {
            _controller = controller;
            _animation = animation;
            _deadCallBack = deadCallBack;
        }
        
        public void OnEnter()
        {
            _animation.DeadAnimation();
            _deadCallBack?.Invoke();
        }

        public void OnExit()
        {
            _currentTime = 0;
        }
        
        public void Tick()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > 5)
            {
                Object.Destroy(_controller.transform.gameObject);
            }
        }
    }
}
