using System.Collections;
using UnityEngine;

namespace CoreBreach.Core
{
    public class CoreDamageFeedback : MonoBehaviour
    {
        [SerializeField] private CoreHealth coreHealth;
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private Color damageColor=Color.red;
        [SerializeField] private float flashDuration=0.12f;

        private Color originalColor;
        private float lastHealth;
        private Coroutine flashRoutine;
        private bool canFlash;

        private void Awake()
        {
            //find core health if it was not assigned
            if (coreHealth ==null)
            {
                coreHealth = GetComponent<CoreHealth>();
            }

            //find a visible renderer if it was not assigned
            if (targetRenderer == null)
            {
                targetRenderer =GetComponentInChildren<Renderer>();
            }

            if (targetRenderer != null && targetRenderer.material.HasProperty("_Color"))
            {
                originalColor = targetRenderer.material.color;
                canFlash = true;
            }

            if (coreHealth != null)
            {
                lastHealth= coreHealth.CurrentHealth;
            }
        }

        private void OnEnable()
        {

            if (coreHealth != null)
            {
                coreHealth.HealthChanged += HandleHealthChanged;
            }
        }

        private void OnDisable()
        {
            //remove event listener
            if (coreHealth !=null)
            {
                coreHealth.HealthChanged -= HandleHealthChanged;
            }
        }

        private void HandleHealthChanged(float currentHealth, float maxHealth)
        {
            //flash only when health goes down
            if (currentHealth < lastHealth && canFlash)
            {
                if (flashRoutine != null)
                {
                    StopCoroutine(flashRoutine);
                }

                flashRoutine = StartCoroutine(FlashDamage());
            }

            lastHealth= currentHealth;
        }

        private IEnumerator FlashDamage()
        {
            if (targetRenderer == null || !canFlash)
            {
                yield break;
            }

            //change color for a short damage effect
            targetRenderer.material.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            targetRenderer.material.color = originalColor;
        }
    }
}