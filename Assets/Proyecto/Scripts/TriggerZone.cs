using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TriggerZone : MonoBehaviour
{
    public event Action onPlayerEnter; // Evento que se dispara al entrar el jugador
    
    private string playerTag = "Player";
    public bool destroyAfterActivation = true;
    private string gameoverSceneName = "GameOver";
    private RestEnemyManager enemyManager;
    private void Start()
    {
        // Conecto con el RestEnemyManager si existe en la escena consiguiendo el objeto por el tag
        GameObject enemyManagerObject = GameObject.FindGameObjectWithTag("ManageEnemyCount");
        if (enemyManagerObject != null)
        {
            Debug.Log($"{gameObject.name}: Conectado a RestEnemyManager.");
            enemyManager = enemyManagerObject.GetComponent<RestEnemyManager>();
        } else
        {
            Debug.LogWarning($"{gameObject.name}: No se encontró RestEnemyManager en la escena.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log($"{gameObject.name}: Jugador ha entrado en el trigger.");
            onPlayerEnter?.Invoke();

            if (destroyAfterActivation)
            {
                gameObject.SetActive(false);
            }
            // Verifico si hay enemigos restantes usando el RestEnemyManager
            if (other.CompareTag("Player") && enemyManager.enemyCount > 0)
            {
                SceneManager.LoadScene(gameoverSceneName);
            }

        }
    }
}
