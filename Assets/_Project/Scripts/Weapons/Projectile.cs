using CoreBreach.Interfaces;
using CoreBreach.Pooling;
using UnityEngine;

namespace CoreBreach.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed =18f;
        [SerializeField] private float lifeTime =2f;
        [SerializeField] private float damage =10f;

        private Vector3 direction;
        private float timer;
        private ProjectilePool pool;

        public void Initialize(ProjectilePool projectilePool)
        {
            pool = projectilePool;
        }

        public void Launch(Vector3 launchDirection)
        {
            direction= launchDirection.normalized;
            timer=0f;
        }

        private void Update()
        {
            float moveDistance = speed*Time.deltaTime;
            //check the next movement step before moving
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit,moveDistance))
            {
                if (!hit.collider.CompareTag("Player"))
                {
                    DealDamage(hit.collider);
                    ReturnToPool();
                    return;
                }
            }

            transform.position += direction*moveDistance;
            //remove old projectiles
            timer += Time.deltaTime;
            if (timer >= lifeTime)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            DealDamage(other);
            ReturnToPool();
        }

        private void DealDamage(Collider targetCollider)
        {
            
            IDamageable damageable =targetCollider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        private void ReturnToPool()
        {
            if (pool != null)
            {
                pool.ReturnProjectile(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
