using UnityEngine;
using UnityEngine.Rendering; // <--- Necesario para acceder al Volume

public class ZonaDesactivarEfectos : MonoBehaviour
{
    [Header("Arrastra aquí tu Global Volume")]
    public Volume globalVolume;

    [Header("Configuración")]
    public string tagJugador = "Player"; // Asegúrate de que tu XR Origin tenga este Tag

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

    // Opcional: Si quieres que se vuelva a activar si sale de la zona
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            if (globalVolume != null)
            {
                globalVolume.enabled = true;
            }
        }
    }
}