using UnityEngine;

public class SaludJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;
    
    [Header("Referencias")]
    public RelojInteligente relojRef;
    private void Start()
    {
        vidaActual = vidaMaxima;
        // Actualizamos el reloj al empezar
        if(relojRef) relojRef.ActualizarVida(vidaActual);
    }

    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;

        // Evitar negativos
        if (vidaActual < 0) vidaActual = 0;

        // Avisamos al reloj visual
        if(relojRef) relojRef.ActualizarVida(vidaActual);

        
    }
}