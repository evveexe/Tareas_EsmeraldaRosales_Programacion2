using UnityEngine;

public class BaseHealth : MonoBehaviour, Damage
{
    [Header("Base Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Reference")]
    public UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI(); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log($"Base recibió {damage} daño. Vida: {currentHealth}/{maxHealth}");

        UpdateUI(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Game Over");

        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.SetGameState(GameState.GameOver);
        }
    }

   
    public void UpdateUI()  
    {
        if (uiManager != null)
        {
            uiManager.UpdateBaseHealth(currentHealth, maxHealth);
        }
        else
        {
            Debug.LogWarning("UIManager no asignado en BaseHealth");
        }
    }

   
}