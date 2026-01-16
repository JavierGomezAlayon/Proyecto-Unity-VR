using UnityEngine;
// Manejo de enemigos en la escena
public class RestEnemyManager : MonoBehaviour
{
    public int enemyCount = 0;

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
}
