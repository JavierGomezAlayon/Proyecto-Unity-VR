using UnityEngine;
using System.Collections;

// Generador de enemigos kamikaze
public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab; 
    public Transform puntoDeSpawn;   
    public float tiempoEntreOleadas = 5f;

    void Start()
    {
        if (enemigoPrefab == null)
        {
            return; // Detenemos el código para no dar más errores
        }

        if (puntoDeSpawn == null)
        {
            return;
        }
        StartCoroutine(RutinaSpawn());
    }

    IEnumerator RutinaSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreOleadas);
            SpawnEnemigo();
        }
    }

    void SpawnEnemigo()
    {
        // Creamos al enemigo
        Instantiate(enemigoPrefab, puntoDeSpawn.position, puntoDeSpawn.rotation);
        Debug.Log("💀 ¡Ha nacido un nuevo Kamikaze!");
    }
}