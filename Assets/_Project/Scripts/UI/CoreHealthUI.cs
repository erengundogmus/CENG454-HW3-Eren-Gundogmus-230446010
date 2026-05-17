using CoreBreach.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CoreBreach.UI
{
    public class CoreHealthUI : MonoBehaviour
    {
        [SerializeField] private CoreHealth coreHealth;
        [SerializeField] private Slider healthSlider;

        private void OnEnable()
        {
            //listen to core health changes
            if (coreHealth != null)
            {
                coreHealth.HealthChanged += UpdateHealthBar;
            }
        }

        private void Start()
        {
            //set the first UI value
            if (coreHealth != null)
            {
                UpdateHealthBar(coreHealth.CurrentHealth,coreHealth.MaxHealth);
            }
        }

        private void OnDisable()
        {
            //stop listening when disabled
            if (coreHealth != null)
            {
                coreHealth.HealthChanged -=UpdateHealthBar;
            }
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            if (healthSlider == null)
            {
                return;
            }

            //convert health to slider value
            healthSlider.value =currentHealth/ maxHealth;
        }
    }
}
