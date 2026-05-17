using System;
using CoreBreach.Interfaces;
using UnityEngine;

namespace CoreBreach.Core
{
    public class CoreHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth =100f;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;
        public bool IsAlive => CurrentHealth >0f;

        public event Action<float, float> HealthChanged;
        public event Action CoreDestroyed;

        private void Awake()
        {
            CurrentHealth = maxHealth;
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHealth = Mathf.Max(CurrentHealth - amount,0f);

            //notify listeners after health changes.,
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <=0f)
            {
                CoreDestroyed?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
            HealthChanged?.Invoke(CurrentHealth,MaxHealth);
        }
    }
}
