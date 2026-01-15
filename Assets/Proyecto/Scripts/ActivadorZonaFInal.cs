using UnityEngine;

public class ActivadorZonaFinal : MonoBehaviour
{
    [Header("Arrastra aquí a tu Enemigo")]
    public GameObject enemigo; 

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que entró es el Jugador
        // Asegúrate de que tu jugador tenga el Tag "Player"
        if (other.CompareTag("Player"))
        {
            // Activamos al enemigo. Esto dispara su función Start() y empieza la lógica.
            enemigo.SetActive(true);

            // El jugador permanece quieto en la zona
            other.GetComponent<Movimientopersonaje>().enabled = false; 
        }
    }
}