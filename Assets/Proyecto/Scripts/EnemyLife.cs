using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("Ajustes de Salud")]
    public float health = 20f;
    public bool destroyOnDeath = true;

    [Header("Efectos (Opcional)")]
    public GameObject deathEffectPrefab; // Prefab de explosión de partículas

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