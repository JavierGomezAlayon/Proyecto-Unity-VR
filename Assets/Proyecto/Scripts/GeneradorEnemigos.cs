using UnityEngine;
using System.Collections;

public class GeneradorEnemigos : MonoBehaviour
{
    [Header("Arrastra aquí el archivo AZUL de la carpeta")]
    public GameObject enemigoPrefab; 
    
    [Header("Arrastra aquí el objeto vacío de la escena")]
    public Transform puntoDeSpawn;   
    
    public float tiempoEntreOleadas = 5f;

    void Start()
    {
        if (enemigoPrefab == null)
        {
            Debug.LogError("⛔ ERROR: ¡El Generador no tiene el 'Enemigo Prefab' asignado!");
            return; // Detenemos el código para no dar más errores
        }

        if (puntoDeSpawn == null)
        {
            Debug.LogError("⛔ ERROR: ¡El Generador no tiene puesto el 'Punto De Spawn'!");
            return;
        }

        Debug.Log("✅ Generador listo. Empezando cuenta atrás...");
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