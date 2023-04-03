using GameFolders.Scripts.Abstracts.Animations;
using GameFolders.Scripts.Abstracts.Movements;
using GameFolders.Scripts.Abstracts.StateMachines;

namespace GameFolders.Scripts.Concretes.StateMachines.EnemyStates
{
    public class ChasePlayer : IState
    {
        private IMover _mover;
        private IFlip _flip;
        private IMyAnimation _animation;
        private IStopEdge _stopEdge;
        private System.Func<bool> _isPlayerRightSide;
        
        public ChasePlayer(IMover mover, IFlip flip, IMyAnimation animation, IStopEdge stopEdge, System.Func<bool> isPlayerRightSide)
        {
            _mover = mover;
            _flip = flip;
            _animation = animation;
            _stopEdge = stopEdge;
            _isPlayerRightSide = isPlayerRightSide;
        }
        
        
        public void OnEnter()
        {
            _animation.MoveAnimation(1f);
        }

        public void OnExit()
        {
            _animation.MoveAnimation(0f);
        }
        
        public void Tick()
        {
            if (_stopEdge.ReachEdge())
            {
                _animation.MoveAnimation(0f);
                return;
            }
            
            if (_isPlayerRightSide.Invoke())
            {
                _mover.Tick(1.2f);
                _flip.FlipCharacter(1f);
            }
            else
            {
                _mover.Tick(-1.2f);
                _flip.FlipCharacter(-1f);
            }
        }
    }
}
