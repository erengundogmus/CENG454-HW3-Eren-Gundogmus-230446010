using UnityEngine;

namespace CoreBreach.Weapons
{
    public class WeaponUpgradePickup : MonoBehaviour
    {
        private enum UpgradeType
        {
            Damage,
            FireRate,
            FullUpgrade
        }

        [SerializeField] private UpgradeType upgradeType =UpgradeType.FullUpgrade;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            PlayerWeaponUpgrades upgrades =other.GetComponentInParent<PlayerWeaponUpgrades>();

            if (upgrades == null)
            {
                return;
            }

            //apply selected upgrade
            if (upgradeType == UpgradeType.Damage)
            {
                upgrades.ApplyDamageBoost();
            }
            else if (upgradeType == UpgradeType.FireRate)
            {
                upgrades.ApplyFireRateBoost();
            }
            else
            {
                upgrades.ApplyFullUpgrade();
            }

            Destroy(gameObject);
        }
    }
}
