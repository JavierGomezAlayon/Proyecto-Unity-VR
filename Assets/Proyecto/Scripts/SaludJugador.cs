using UnityEngine;
using System.Collections; // <--- IMPORTANTE: Necesario para IEnumerator
using UnityEngine.SceneManagement;

public class SaludJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;
    
    [Header("Referencias")]
    public RelojInteligente relojRef;

    [Header("Sistema de Escudo")]
    public GameObject efectoEscudo; 
    private bool esInvulnerable = false;

    private void Start()
    {
        vidaActual = vidaMaxima;
        if(relojRef) relojRef.ActualizarVida(vidaActual);
        
        // Al empezar, nos aseguramos de que el escudo visual esté apagado
        if(efectoEscudo) efectoEscudo.SetActive(false);
    }

    public void RecibirDaño(int daño)
    {
        // SI EL ESCUDO ESTÁ ACTIVO, IGNORAMOS EL GOLPE
        if (esInvulnerable) 
        {
            Debug.Log("¡Golpe bloqueado por el escudo!");
            return; 
        }

        vidaActual -= daño;

        if (vidaActual <= 0) {
            vidaActual = 0;
            SceneManager.LoadScene("GameOver");
        }
        if(relojRef) relojRef.ActualizarVida(vidaActual);
    }

    
    public void ActivarInmunidadTemporal(float duracion)
    {
        if (!esInvulnerable)
        {
            StartCoroutine(RutinaInmunidad(duracion));
        }
    }

    // La rutina que cuenta el tiempo
    IEnumerator RutinaInmunidad(float tiempo)
    {
        esInvulnerable = true;
        Debug.Log(">>> ESCUDO ACTIVADO <<<");

        // Activamos efecto visual
        if (efectoEscudo) efectoEscudo.SetActive(true);

        // Esperamos X segundos
        yield return new WaitForSeconds(tiempo);

        // Desactivamos todo
        esInvulnerable = false;
        if (efectoEscudo) efectoEscudo.SetActive(false);
        Debug.Log(">>> ESCUDO DESACTIVADO <<<");
    }
}