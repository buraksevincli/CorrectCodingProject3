using GameFolders.Scripts.Abstracts.Combats;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class DeadOnTouchController : MonoBehaviour
    {
        private IAttacker _attacker;

        private void Awake()
        {
            _attacker = GetComponent<IAttacker>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            IHealth health = col.GetComponent<IHealth>();

            if (health != null)
            {
                health.TakeHit(_attacker);
            }
        }
    }
}

