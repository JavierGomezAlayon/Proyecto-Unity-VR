using UnityEngine;
using System;

public class TriggerZone : MonoBehaviour
{
    public event Action onPlayerEnter; // Evento que se dispara al entrar el jugador
    
    private string playerTag = "Player";
    public bool destroyAfterActivation = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log($"{gameObject.name}: Jugador ha entrado en el trigger.");
            onPlayerEnter?.Invoke();

            if (destroyAfterActivation)
                // Desactivar el trigger para que no se repita
                gameObject.SetActive(false);
            

        }
    }
}
