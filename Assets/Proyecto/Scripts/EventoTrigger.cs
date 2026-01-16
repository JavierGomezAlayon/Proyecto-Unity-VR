using UnityEngine;
using UnityEngine.Events;

// Script general para eventos con trigger
public class EventoTrigger : MonoBehaviour
{
    [Header("Configuración")]
    public string tagJugador = "Player";
    public bool soloUnaVez = true;

    [Header("¿Qué debe pasar?")]
    public UnityEvent alEntrar;
    public UnityEvent alSalir;

    private bool yaActivado = false;

    private void OnTriggerEnter(Collider other)
    {
        // Si es de un solo uso y ya se usó, no hacemos nada
        if (soloUnaVez && yaActivado) return;

        // Comprobamos si es el Jugador
        if (other.CompareTag(tagJugador))
        {
            Debug.Log("Trigger activado por: " + other.name);
            
            // Ejecutamos todas las acciones de la lista
            alEntrar.Invoke();

            yaActivado = true;
        }
    }
}