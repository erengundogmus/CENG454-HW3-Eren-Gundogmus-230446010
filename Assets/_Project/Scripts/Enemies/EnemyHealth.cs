using CoreBreach.Interfaces;
using UnityEngine;

namespace CoreBreach.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth=30f;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;
        public bool IsAlive => CurrentHealth > 0f;

        private void Awake()
        {
            CurrentHealth=maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);

            //remove enemy when health reaches zero
            if (CurrentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHealth = Mathf.Min(CurrentHealth+ amount, MaxHealth);
        }
    }
}
