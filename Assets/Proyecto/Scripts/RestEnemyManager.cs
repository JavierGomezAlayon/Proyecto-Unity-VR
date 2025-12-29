using UnityEngine;
using UnityEngine.SceneManagement;

public class RestEnemyManager : MonoBehaviour
{
    // no quiero que se visible en el inspector
    //[HideInInspector]
    public int enemyCount = 0;
    private string gameoverSceneName = "GameOver";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void AddEnemy()
    {
        enemyCount++;
        Debug.Log("Enemy added. Total enemies: " + enemyCount);
    }
    public void EnemyDefeated()
    {
        enemyCount--;
        Debug.Log("Enemy defeated. Remaining enemies: " + enemyCount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyCount > 0)
        {
            SceneManager.LoadScene(gameoverSceneName);
        }
    }
}
