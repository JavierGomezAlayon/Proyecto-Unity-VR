using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // <--- NECESARIO PARA CAMBIAR DE ESCENA

public class BossLife : MonoBehaviour
{
    [Header("Ajustes de Salud")]
    public float health = 100f;

    [Header("Configuración de Escena")]
    public string sceneToLoad = "Menu";
    public float delayBeforeLoading = 1f;

    [Header("Efectos (Opcional)")]
    public GameObject deathEffectPrefab;

    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        animator.SetTrigger("Herido");

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (animator != null)
        {
            animator.SetTrigger("Morir");
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        StartCoroutine(GoToMenuRoutine());
    }

    IEnumerator GoToMenuRoutine()
    {
        Debug.Log("El Boss ha muerto. Esperando " + delayBeforeLoading + " segundos...");
        
        // Esperamos a que termine la animación dramática
        yield return new WaitForSeconds(delayBeforeLoading);

        // Cargamos la escena
        Debug.Log("Cargando escena: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}