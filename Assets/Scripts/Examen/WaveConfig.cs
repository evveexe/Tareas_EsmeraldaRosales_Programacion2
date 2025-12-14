using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewWaveConfig", menuName = "Wave System/Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    [System.Serializable]
    public class WaveEntry
    {
        public EnemyProfile enemyProfile;
        public int count = 10;
        public float timeBetweenSpawns = 1f;
    }

    [Header("Wave Settings")]   
    public WaveEntry[] enemies;
    public float timeBeforeNextWave = 5f;

    [Header("Dificultad")]
    [Range(0.5f, 2f)] public float difficultyMultiplier = 1f;
}