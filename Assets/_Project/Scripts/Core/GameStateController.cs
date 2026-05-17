using UnityEngine;

namespace CoreBreach.Core
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private CoreHealth coreHealth;
        [SerializeField] private GameObject losePanel;

        private bool gameEnded;

        private void Start()
        {
            //start the game in normal time
            Time.timeScale =1f;

            if (losePanel != null)
            {
                losePanel.SetActive(false);
            }
        }

        private void OnEnable()
        {
            // listen for core destroy event
            if (coreHealth != null)
            {
                coreHealth.CoreDestroyed += HandleCoreDestroyed;
            }
        }

        private void OnDisable()
        {

            if (coreHealth != null)
            {
                coreHealth.CoreDestroyed -=HandleCoreDestroyed;
            }
        }

        private void HandleCoreDestroyed()
        {
            if (gameEnded)
            {
                return;
            }

            gameEnded = true;

            //show lose screen
            if (losePanel != null)
            {
                losePanel.SetActive(true);
            }

            //stop the game after lose
            Time.timeScale =0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible =true;
        }
    }
}
