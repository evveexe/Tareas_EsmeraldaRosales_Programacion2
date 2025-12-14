using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI baseHealthText;
    public UnityEngine.UI.Button startWaveButton;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public UnityEngine.UI.Button restartButton;

    void Start()
    {
        if (startWaveButton != null)
            startWaveButton.onClick.AddListener(OnStartWaveButtonClicked);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonClicked);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateAllUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void OnStartWaveButtonClicked()
    {
        if (WaveManager.Instance != null && WaveManager.Instance.currentGameState == GameState.Preparation)
        {
            WaveManager.Instance.StartNextWave();
        }
    }

    void OnRestartButtonClicked()
    {
        RestartGame();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateWaveInfo(int currentWave, int totalWaves)
    {
        if (waveText != null)
            waveText.text = $"Oleada: {Mathf.Min(currentWave + 1, totalWaves)}/{totalWaves}";
    }

    public void UpdateGameState(GameState state)
    {
        if (stateText != null)
        {
            string estado = "";
            switch (state)
            {
                case GameState.Preparation: estado = "Preparación"; break;
                case GameState.Spawning: estado = "Generando"; break;
                case GameState.WaveInProgress: estado = "Oleada en curso"; break;
                case GameState.WaveComplete: estado = "Oleada completada"; break;
                case GameState.GameOver: estado = "¡Game Over!"; break;
                case GameState.Victory: estado = "¡Victoria!"; break;
            }
            stateText.text = $"Estado: {estado}";
        }

        if (startWaveButton != null)
            startWaveButton.gameObject.SetActive(state == GameState.Preparation);

        if (gameOverPanel != null)
        {
            bool showPanel = (state == GameState.GameOver || state == GameState.Victory);
            gameOverPanel.SetActive(showPanel);

            if (showPanel && gameOverText != null)
            {
                if (state == GameState.GameOver)
                {
                    gameOverText.text = "¡PERDISTExd";
                    gameOverText.color = Color.red; 
                }
                else if (state == GameState.Victory)
                {
                    gameOverText.text = "¡VICTORIA!\nCompletaste todas las oleadas";
                    gameOverText.color = Color.green; 
                }
            }
        }
    }

    public void UpdateEnemiesCount(int count)
    {
        if (enemiesText != null)
            enemiesText.text = $"Enemigos: {count}";
    }

    public void UpdateBaseHealth(int current, int max)
    {
        if (baseHealthText != null)
        {
            baseHealthText.text = $"Base: {current}/{max}";

            float healthPercent = (float)current / max;
            if (healthPercent <= 0.3f)
                baseHealthText.color = Color.red;
            else if (healthPercent <= 0.6f)
                baseHealthText.color = Color.yellow;
            else
                baseHealthText.color = Color.green;
        }
    }

    public void UpdateAllUI()
    {
        if (WaveManager.Instance != null)
        {
            UpdateWaveInfo(WaveManager.Instance.currentWaveIndex, WaveManager.Instance.waveConfigs.Length);
            UpdateGameState(WaveManager.Instance.currentGameState);
            UpdateEnemiesCount(WaveManager.Instance.enemiesRemaining);

            BaseHealth baseHealth = FindAnyObjectByType<BaseHealth>();
            if (baseHealth != null)
                UpdateBaseHealth(baseHealth.currentHealth, baseHealth.maxHealth);
        }
    }
}