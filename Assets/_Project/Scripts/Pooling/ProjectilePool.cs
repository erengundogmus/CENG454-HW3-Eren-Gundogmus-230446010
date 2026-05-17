using System.Collections.Generic;
using CoreBreach.Weapons;
using UnityEngine;

namespace CoreBreach.Pooling
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int initialSize =20;

        private readonly Queue<Projectile> pooledProjectiles =new Queue<Projectile>();

        private void Awake()
        {
            // ccreate projectiles before gameplay starts
            for (int i =0; i < initialSize; i++)
            {
                Projectile projectile = CreateProjectile();
                pooledProjectiles.Enqueue(projectile);
            }
        }

        public Projectile GetProjectile(Vector3 position, Quaternion rotation)
        {
            if (pooledProjectiles.Count ==0)
            {
                Projectile newProjectile = CreateProjectile();
                pooledProjectiles.Enqueue(newProjectile);
            }

            Projectile projectile = pooledProjectiles.Dequeue();
            projectile.transform.SetPositionAndRotation(position, rotation);
            projectile.gameObject.SetActive(true);

            return projectile;
        }

        public void ReturnProjectile(Projectile projectile)
        {
            if (projectile == null)
            {
                return;
            }

            //hide projectile instead of destroying it
            projectile.gameObject.SetActive(false);
            projectile.transform.SetParent(transform);
            pooledProjectiles.Enqueue(projectile);
        }

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(projectilePrefab,transform);
            projectile.Initialize(this);
            projectile.gameObject.SetActive(false);
            return projectile;
        }
    }
}
