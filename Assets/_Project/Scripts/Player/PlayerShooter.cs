using CoreBreach.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreBreach.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
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
                nextFireTime = Time.time + fireRate;
            }
        }

        private void Shoot()
        {
            if (projectilePrefab== null || firePoint == null)
            {
                return;
            }
            //spawn the projectile from the fire point
            Projectile projectile = Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.LookRotation(firePoint.forward)
            );

            projectile.Launch(firePoint.forward);
        }
    }
}
