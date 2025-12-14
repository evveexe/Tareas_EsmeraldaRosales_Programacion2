using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, Damage
{
    [Header("Stats from Profile")]
    public EnemyProfile profile;

    private int currentHealth;
    private NavMeshAgent agent;
    private Renderer meshRenderer;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponentInChildren<Renderer>();

        if (profile != null)
        {
            currentHealth = profile.maxHealth;
            agent.speed = profile.moveSpeed;

            if (profile.material != null)
                meshRenderer.material = profile.material;
            else
                meshRenderer.material.color = profile.enemyColor;
        }
        else
        {
            Debug.LogError("Enemy Profile no asignado!");
        }

        SetTarget();
    }

    void SetTarget()
    {
        GameObject playerBase = GameObject.FindGameObjectWithTag("Base");
        if (playerBase != null && agent != null)
        {
            agent.SetDestination(playerBase.transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"{name} recibió {damage} daño. Vida: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Notificar al WaveManager
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyDefeated(this);
        }

        Destroy(gameObject, 0.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base") && !isDead)
        {
            // Aplicar daño a la base
            BaseHealth baseHealth = other.GetComponent<BaseHealth>();
            if (baseHealth != null && profile != null)
            {
                baseHealth.TakeDamage(profile.damage);
            }

            // Destruir enemigo (notificar que murió)
            Die();
        }
    }
}