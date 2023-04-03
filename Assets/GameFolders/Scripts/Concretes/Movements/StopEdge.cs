using GameFolders.Scripts.Abstracts.Movements;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Movements
{
    [RequireComponent(typeof(Collider2D))]
    public class StopEdge : MonoBehaviour,IStopEdge
    {
        [SerializeField] private float distance = 0.1f;
        [SerializeField] private LayerMask _layerMask;

        private Collider2D _collider2D;
        private float _direction;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _direction = transform.localScale.x;
        }

        public bool ReachEdge()
        {
            float x = GetXPosition();
            float y = _collider2D.bounds.min.y;

            Vector2 origin = new Vector2(x, y);

            RaycastHit2D hit2D = Physics2D.Raycast(origin, Vector2.down, distance, _layerMask);

            Debug.DrawRay(origin, Vector2.down * distance, Color.red);

            bool result = hit2D.collider != null;
            
            if (result)
            {
                //_direction *= -transform.localScale.x;
                return false;
            }

            return true;
        }

        private float GetXPosition()
        {
            return _direction == 1f ? _collider2D.bounds.max.x + 0.1f : _collider2D.bounds.min.x - 0.1f;
        }
    }
}

