using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TriggerFinal : MonoBehaviour
{
    private string playerTag = "Player";
    private string FinalScene = "FinalScene";
    private string gameoverSceneName = "GameOver";
    private RestEnemyManager enemyManager;

    void Start()
    {
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
            Debug.Log($"{gameObject.name}: Jugador ha terminado el juego.");

            // Verifico si hay enemigos restantes usando el RestEnemyManager
            if (enemyManager.enemyCount > 0)
            {
                SceneManager.LoadScene(gameoverSceneName);
            } else
            {
                SceneManager.LoadScene(FinalScene);
            }

        }
    }
}
