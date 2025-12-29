using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [Header("Referencia al Trigger de inicio")]
    public GameObject triggerTargetStart; // El trigger que este spawner debe escuchar
    [Header("Referencia al Trigger final")]
    public GameObject triggerTargetFinal; // El trigger que este spawner debe escuchar

    [Header("Enemigo")]
    public GameObject enemyPrefab;

    private TriggerZone triggerScriptStart;
    private RestEnemyManager triggerScriptFinal;
    public void Start()
    {
        triggerScriptStart = triggerTargetStart.GetComponent<TriggerZone>();
        if (triggerScriptStart == null)
        {
            Debug.LogWarning($"{gameObject.name}: No se encontró TriggerZone en el objeto asignado como trigger inicio.");
        }
        triggerScriptStart.onPlayerEnter += SpawnEnemy;
    }
    private void OnEnable()
    {
        if (triggerScriptStart != null)
        {
            triggerScriptStart.onPlayerEnter += SpawnEnemy;
        }
    }

    private void OnDisable()
    {
        // Desuscribirse al destruir/desactivar el objeto para evitar errores
        if (triggerScriptStart != null)
        {
            triggerScriptStart.onPlayerEnter -= SpawnEnemy;
        }
    }

    private void SpawnEnemy()
    {
        Debug.Log($"{gameObject.name}: ¡Recibida señal para crear enemigo!");
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            // le doy al hijo el gameobject del trigger final para que pueda notificarle al morir
            EnemyLife enemyLifeScript = enemyPrefab.GetComponent<EnemyLife>();
            if (enemyLifeScript != null)
            {
                enemyLifeScript.enemyManager = triggerTargetFinal;
            }
            // Debug.Log($"{gameObject.name}: ¡Enemigo creado tras recibir la señal!");
            enemyLifeScript.añadirEnemigo();
        }
    }
}
