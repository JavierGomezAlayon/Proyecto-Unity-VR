using UnityEngine;
using UnityEngine.SceneManagement;

public class EnviarA : MonoBehaviour
{
    [Header("Configuración")]
    public string escena = "FinalScene";

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que ha entrado es el Jugador
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(escena);
        }
    }
}