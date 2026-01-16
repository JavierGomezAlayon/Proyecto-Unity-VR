using UnityEngine;
using UnityEngine.Rendering; // <--- Necesario para acceder al Volume

// Zona para desactivar el efecto tunel cuando el jugador se para
public class ZonaDesactivarEfectos : MonoBehaviour
{
    [Header("Arrastra aquí tu Global Volume")]
    public Volume globalVolume;

    [Header("Configuración")]
    public string tagJugador = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si lo que ha entrado es el Jugador
        if (other.CompareTag(tagJugador))
        {
            if (globalVolume != null)
            {
                // Apagamos el componente del volumen completamente
                globalVolume.enabled = false;
                
                Debug.Log("¡El jugador ha llegado a la zona! Volumen desactivado.");
            }
        }
    }
}