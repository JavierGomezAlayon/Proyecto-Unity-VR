using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("Ajustes de Salud")]
    public float health = 20f;
    public bool destroyOnDeath = true;

    [Header("Efectos (Opcional)")]
    public GameObject deathEffectPrefab; // Prefab de explosión de partículas

    private GameObject enemyManager;
    private RestEnemyManager scriptEnemyCounter;

    // Para efecto de daño


    private void Start()
    {
        // Busco el objeto que maneja el conteo de enemigos en la escena
        enemyManager = GameObject.FindGameObjectWithTag("ManageEnemyCount");
        if (enemyManager == null)
        {
            Debug.LogWarning("EnemyLife: No se encontró ningún objeto con el tag 'ManageEnemyCount' en la escena.");
        }
        añadirEnemigo();

        // Obtengo el material del objeto para ponerlo en rojo al recibir daño

    }
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

    private void Die()
    {
        if (scriptEnemyCounter == null)
        {
            scriptEnemyCounter = enemyManager.GetComponent<RestEnemyManager>();
            if (scriptEnemyCounter == null)
            {
                Debug.LogWarning("EnemyLife: No se encontró RestEnemyManager en el objeto asignado.");
            }
        } 
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