using CoreBreach.Core;
using CoreBreach.Interfaces;
using UnityEngine;

namespace CoreBreach.Enemies
{
    public class EnemyCoreAttacker : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float attackRange =3f;
        [SerializeField] private float damage =10f;
        [SerializeField] private float attackCooldown =1f;

        private IDamageable targetDamageable;
        private float nextAttackTime;

        private void Start()
        {
            if (target == null)
            {
                GameObject coreObject = GameObject.Find("Core_Objective");

                if (coreObject != null)
                {
                    SetTarget(coreObject.transform);
                }
            }
        }

        private void Update()
        {
            if (target == null || targetDamageable == null)
            {
                return;
            }

            Vector3 enemyPosition= transform.position;
            Vector3 targetPosition = target.position;

            enemyPosition.y =0f;
            targetPosition.y =0f;

            float distance = Vector3.Distance(enemyPosition, targetPosition);

            //attack when close enough to the core
            if (distance <= attackRange && Time.time >= nextAttackTime)
            {
                targetDamageable.TakeDamage(damage);
                nextAttackTime = Time.time+attackCooldown;
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;

            CoreHealth coreHealth =target.GetComponentInParent<CoreHealth>();

            if (coreHealth == null)
            {
                coreHealth =target.GetComponentInChildren<CoreHealth>();
            }

            targetDamageable =coreHealth;
        }
    }
}