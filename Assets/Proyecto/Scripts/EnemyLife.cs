using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("Ajustes de Salud")]
    public float health = 20f;
    public bool destroyOnDeath = true;
    [Header("Trigger de final de zona")]
    public GameObject enemyManager; // Referencia al gestor de enemigos

    [Header("Efectos (Opcional)")]
    public GameObject deathEffectPrefab; // Prefab de explosión de partículas
    private RestEnemyManager scriptEnemyCounter;
    public void añadirEnemigo()
    {
        scriptEnemyCounter = enemyManager.GetComponent<RestEnemyManager>();
        if (scriptEnemyCounter == null)
        {
            Debug.LogWarning("EnemyLife: No se encontró RestEnemyManager en el objeto asignado.");
        } else
        {
            scriptEnemyCounter.AddEnemy();
        }
    }
    // Esta función será llamada por la bala al impactar
    public void TakeDamage(float amount)
    {
        health -= amount;
        // Debug.Log($"{gameObject.name} recibió {amount} de daño. Vida restante: {health}");

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (scriptEnemyCounter != null)
        {
            scriptEnemyCounter.EnemyDefeated();
        }
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        
        // Debug.Log("Enemigo eliminado");
    }
}