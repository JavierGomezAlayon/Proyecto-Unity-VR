using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("LookAtPlayer: No se encontró ningún objeto con el tag 'Player' en la escena.");
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Hacer que el objeto mire hacia el jugador
        transform.LookAt(player.transform);
    }
}
