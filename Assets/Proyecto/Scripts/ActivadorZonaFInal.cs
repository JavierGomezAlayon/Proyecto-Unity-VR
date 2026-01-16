using UnityEngine;

// Script para que el jugador pare en frente del enemigo final
public class ActivadorZonaFinal : MonoBehaviour
{
    [Header("Arrastra aquí a tu Enemigo")]
    public GameObject enemigo; 

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que entró es el Jugador
        if (other.CompareTag("Player"))
        {
            // Activamos al enemigo. Esto dispara su función Start() y empieza la lógica.
            enemigo.SetActive(true);

            // El jugador permanece quieto en la zona
            other.GetComponent<Movimientopersonaje>().enabled = false; 
        }
    }
}