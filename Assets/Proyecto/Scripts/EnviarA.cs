using UnityEngine;
using UnityEngine.SceneManagement;

// Script para enviar al jugador a otra escena con un trigger
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