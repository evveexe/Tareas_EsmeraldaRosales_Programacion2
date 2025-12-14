using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyProfile", menuName = "Wave System/Enemy Profile")]
public class EnemyProfile : ScriptableObject
{
    [Header("Estadisticzs")]
    public int maxHealth = 100;
    public float moveSpeed = 5f;
    public int damage = 10;
    public int pointValue = 10;

    [Header("Visuals")]
    public GameObject prefab;
    public Material material;
    public Color enemyColor = Color.red;

    [Header("Spawn Settings")]
    public float spawnDelay = 0.5f;
}