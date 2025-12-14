using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Wave Configuration")]
    public WaveConfiguration[] waveConfigs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("UI Reference")]
    public UIManager uiManager;

    // Públicos para que UI pueda acceder
    [HideInInspector] public GameState currentGameState = GameState.Preparation;
    [HideInInspector] public int currentWaveIndex = 0;
    [HideInInspector] public int enemiesRemaining = 0;

    private List<EnemyBase> activeEnemies = new List<EnemyBase>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SetGameState(GameState.Preparation);
        UpdateUI();
    }

    public void StartNextWave()
    {
        if (currentGameState != GameState.Preparation &&
            currentGameState != GameState.WaveComplete)
        {
            Debug.Log("No se puede iniciar wave ahora. Estado: " + currentGameState);
            return;
        }

        if (currentWaveIndex >= waveConfigs.Length)
        {
            SetGameState(GameState.Victory);
            return;
        }

        StartCoroutine(SpawnWaveCoroutine(waveConfigs[currentWaveIndex]));
    }

    private IEnumerator SpawnWaveCoroutine(WaveConfiguration waveConfig)
    {
        SetGameState(GameState.Spawning);

        foreach (var entry in waveConfig.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                SpawnEnemy(entry.enemyProfile);
                yield return new WaitForSeconds(entry.timeBetweenSpawns);
            }
        }

        SetGameState(GameState.WaveInProgress);

        // Esperar a que todos los enemigos sean derrotados o lleguen a la base
        yield return new WaitUntil(() => enemiesRemaining <= 0);

        // Solo continuar si NO estamos en GameOver
        if (currentGameState != GameState.GameOver)
        {
            SetGameState(GameState.WaveComplete);
            currentWaveIndex++;
            UpdateUI();

            // Si completamos todas las waves
            if (currentWaveIndex >= waveConfigs.Length)
            {
                SetGameState(GameState.Victory);
            }
            else
            {
                // Preparar siguiente oleada
                yield return new WaitForSeconds(waveConfig.timeBeforeNextWave);
                SetGameState(GameState.Preparation);
            }
        }
    }

    private void SpawnEnemy(EnemyProfile profile)
    {
        if (spawnPoints.Length == 0 || profile.prefab == null) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = Instantiate(profile.prefab, spawnPoint.position, spawnPoint.rotation);

        EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.profile = profile;
            activeEnemies.Add(enemy);
            enemiesRemaining++;
            UpdateUI();
        }
    }

    public void EnemyDefeated(EnemyBase enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            enemiesRemaining--;
            UpdateUI();

            // Verificar si terminó la wave
            CheckWaveCompletion();
        }
    }

    private void CheckWaveCompletion()
    {
        if (enemiesRemaining <= 0 && currentGameState == GameState.WaveInProgress)
        {
            SetGameState(GameState.WaveComplete);
        }
    }

    public void SetGameState(GameState newState)
    {
        // No cambiar si ya estamos en GameOver
        if (currentGameState == GameState.GameOver && newState != GameState.GameOver)
            return;

        currentGameState = newState;
        Debug.Log("Estado del juego: " + newState);
        UpdateUI();

        // Si es GameOver, detener todas las corrutinas
        if (newState == GameState.GameOver)
        {
            StopAllCoroutines();

            // Destruir todos los enemigos restantes
            foreach (var enemy in activeEnemies.ToArray())
            {
                if (enemy != null)
                    Destroy(enemy.gameObject);
            }
            activeEnemies.Clear();
            enemiesRemaining = 0;
            UpdateUI();
        }
    }

    // Método que actualiza TODA la UI
    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateWaveInfo(currentWaveIndex, waveConfigs.Length);
            uiManager.UpdateGameState(currentGameState);
            uiManager.UpdateEnemiesCount(enemiesRemaining);
        }
    }

    // Para testing en el inspector
    public void TestSpawnSingleEnemy(EnemyProfile profile)
    {
        if (currentGameState == GameState.Preparation)
        {
            SpawnEnemy(profile);
        }
    }

    // DEBUG: Tecla espacio para iniciar oleada
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentGameState != GameState.GameOver)
        {
            StartNextWave();
        }
    }
}