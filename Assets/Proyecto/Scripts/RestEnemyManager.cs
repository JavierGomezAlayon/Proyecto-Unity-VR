using UnityEngine;


public class RestEnemyManager : MonoBehaviour
{
    // no quiero que se visible en el inspector
    //[HideInInspector]
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
