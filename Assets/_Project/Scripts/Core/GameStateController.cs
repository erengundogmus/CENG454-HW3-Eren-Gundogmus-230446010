using CoreBreach.Waves;
using UnityEngine;

namespace CoreBreach.Core
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private CoreHealth coreHealth;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;

        private bool gameEnded;

        private void Start()
        {
            //start the game in normal time
            Time.timeScale =1f;

            if (losePanel != null)
            {
                losePanel.SetActive(false);
            }

            if (winPanel != null)
            {
                winPanel.SetActive(false);
            }
        }

        private void OnEnable()
        {
            // listen for core destroy event
            if (coreHealth != null)
            {
                coreHealth.CoreDestroyed += HandleCoreDestroyed;
            }

            if (enemySpawner != null)
            {
                enemySpawner.AllWavesCleared += HandleAllWavesCleared;
            }
        }

        private void OnDisable()
        {

            if (coreHealth != null)
            {
                coreHealth.CoreDestroyed -=HandleCoreDestroyed;
            }

            if (enemySpawner != null)
            {
                enemySpawner.AllWavesCleared -= HandleAllWavesCleared;
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

            StopGame();
        }

        private void HandleAllWavesCleared()
        {
            if (gameEnded)
            {
                return;
            }

            gameEnded = true;

            //show win screen
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }

            StopGame();
        }

        private void StopGame()
        {
            //stop the game after win or lose
            Time.timeScale =0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible =true;
        }
    }
}