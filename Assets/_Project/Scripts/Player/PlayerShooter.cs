using CoreBreach.Pooling;
using CoreBreach.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreBreach.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private ProjectilePool projectilePool;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate =0.25f;

        private float nextFireTime;

        private void Update()
        {
            if (Mouse.current == null)
            {
                return;
            }

            //fire while the left mouse button is held
            if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time+fireRate;
            }
        }

        private void Shoot()
        {
            if (projectilePool == null || firePoint == null)
            {
                return;
            }

            //get projectile from the pool
            Projectile projectile =projectilePool.GetProjectile(
                firePoint.position,
                Quaternion.LookRotation(firePoint.forward)
            );

            projectile.Launch(firePoint.forward);
        }
    }
}
