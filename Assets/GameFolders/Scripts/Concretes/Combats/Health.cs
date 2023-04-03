using GameFolders.Scripts.Abstracts.Combats;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Combats
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int currentHealth;

        public bool IsDead => currentHealth < 1;

        public event System.Action<int, int> OnHealthChanged;
        public event System.Action OnDead;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeHit(IAttacker attacker)
        {
            if (IsDead) return;

            currentHealth = Mathf.Max(currentHealth -= attacker.Damage, 0);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (IsDead) OnDead?.Invoke();
        }

        public void Heal(int lifeCount)
        {
            currentHealth = Mathf.Min(currentHealth += lifeCount, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}