using UnityEngine;

namespace CoreBreach.Weapons
{
    public class PlayerWeaponUpgrades : MonoBehaviour
    {
        [SerializeField] private float baseDamage =10f;
        [SerializeField] private float baseFireRate =0.25f;
        [SerializeField] private bool useDamageBoost =false;
        [SerializeField] private bool useFireRateBoost =false;
        [SerializeField] private float damageBonus =5f;
        [SerializeField] private float fireRateMultiplier =0.75f;

        private IWeapon weapon;

        public float CurrentDamage => weapon.Damage;
        public float CurrentFireRate => weapon.FireRate;

        private void Awake()
        {
            BuildWeapon();
        }

        public void ApplyDamageBoost()
        {
            useDamageBoost =true;
            BuildWeapon();
        }

        public void ApplyFireRateBoost()
        {
            useFireRateBoost =true;
            BuildWeapon();
        }

        public void ApplyFullUpgrade()
        {
            useDamageBoost =true;
            useFireRateBoost =true;
            BuildWeapon();
        }

        private void BuildWeapon()
        {
            //start with the base weapon
            weapon = new BasicWeapon(baseDamage, baseFireRate);

            //add optional damage upgrade
            if (useDamageBoost)
            {
                weapon = new DamageBoostDecorator(weapon, damageBonus);
            }

            //add optional fire rate upgrade
            if (useFireRateBoost)
            {
                weapon = new FireRateDecorator(weapon, fireRateMultiplier);
            }
        }
    }
}
