using System;
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

        public event Action<EnemyHealth> Died;

        private bool deathNotified;

        private void Awake()
        {
            CurrentHealth=maxHealth;
            deathNotified=false;
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
                NotifyDeath();
                Destroy(gameObject);
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHealth =Mathf.Min(CurrentHealth+ amount, MaxHealth);
        }

        private void NotifyDeath()
        {
            if (deathNotified)
            {
                return;
            }

            deathNotified=true;
            Died?.Invoke(this);
        }
    }
}