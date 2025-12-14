using System.Collections;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject enemyPrefab;
    public int numberOfWaves = 3;
    public int enemiesPerWave = 5;
    public float timeBetweenEnemies = 1f;
    public float timeBetweenWaves = 5f;

    [Header("Posición de Spawn")]
    public Transform spawnPoint;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
       
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
                       
            for (int enemy = 0; enemy < enemiesPerWave; enemy++)
            {
                
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                                
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
                                              
            yield return new WaitForSeconds(timeBetweenWaves);
        }

            }
}