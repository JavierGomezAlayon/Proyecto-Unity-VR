using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour
{
    [Header("Ajustes de Salud")]
    public float health = 20f;
    public bool destroyOnDeath = true;

    [Header("Efectos (Opcional)")]
    public GameObject deathEffectPrefab;

    private Animator animator;
    private GameObject enemyManager;
    private RestEnemyManager scriptEnemyCounter;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        enemyManager = GameObject.FindGameObjectWithTag("ManageEnemyCount");
        if (enemyManager != null)
        {
            añadirEnemigo();
        }
    }

    public void añadirEnemigo()
    {
        scriptEnemyCounter = enemyManager.GetComponent<RestEnemyManager>();
        if (scriptEnemyCounter != null)
        {
            scriptEnemyCounter.AddEnemy();
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // Si ya está muerto, ignoramos daño extra

        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // 1. Notificar al manager
        if (scriptEnemyCounter == null && enemyManager != null)
        {
            scriptEnemyCounter = enemyManager.GetComponent<RestEnemyManager>();
        } 
        if (scriptEnemyCounter != null)
        {
            scriptEnemyCounter.EnemyDefeated();
        }

        Kamikaze kamikazeScript = GetComponent<Kamikaze>();
    
        if (kamikazeScript != null)
        {
            // Si es un kamikaze, que explote él (y salimos de esta función)
            kamikazeScript.Detonar();
            return; 
        }

        // 2. Efectos visuales (Partículas)
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // 3. ANIMACIÓN DE MUERTE
        if (animator != null)
        {
            animator.SetTrigger("Morir");
        }

        // 4. Desactivar colisiones para que no bloquee balas ni al jugador mientras cae
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // 5. Destruir el objeto con retraso (para que se vea la animación)
        if (destroyOnDeath)
        {
            Destroy(gameObject, 5f); 
        }
    }
}