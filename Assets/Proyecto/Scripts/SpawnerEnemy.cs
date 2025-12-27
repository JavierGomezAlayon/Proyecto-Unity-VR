using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [Header("Referencia al Trigger")]
    public TriggerZone triggerTarget; // El trigger que este spawner debe escuchar

    [Header("Enemigo")]
    public GameObject enemyPrefab;

    private void OnEnable()
    {
        if (triggerTarget != null)
        {
            triggerTarget.onPlayerEnter += SpawnEnemy;
        }
    }

    private void OnDisable()
    {
        // Desuscribirse al destruir/desactivar el objeto para evitar errores
        if (triggerTarget != null)
        {
            triggerTarget.onPlayerEnter -= SpawnEnemy;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            // Debug.Log($"{gameObject.name}: ¡Enemigo creado tras recibir la señal!");
        }
    }
}
