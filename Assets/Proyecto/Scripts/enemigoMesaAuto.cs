using UnityEngine;
using System.Collections.Generic;

public class EnemigoMesaAuto : MonoBehaviour
{
    [Header("Plataformas")]
    public GameObject[] plataformas;

    [Header("Configuración")]
    public float tiempoCambio = 3.0f;

    // Lista privada donde guardaremos las coordenadas reales de los SpawnPoints
    private List<Transform> puntosReales = new List<Transform>();
    
    private float temporizador;

    private GameObject enemigoPrefab;

    void Start()
    {
        enemigoPrefab = this.gameObject;
        temporizador = tiempoCambio;
        // Recorremos cada plataforma que arrastraste y buscamos su hijo "SpawnPoint"
        foreach (GameObject mesa in plataformas)
        {
            Transform puntoEncontrado = mesa.transform.Find("SpawnPoint");

            if (puntoEncontrado != null)
            {
                puntosReales.Add(puntoEncontrado);
            }
        }

        Teletransportar();
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0)
        {
            Teletransportar();
            temporizador = tiempoCambio;
        }
    }

    void Teletransportar()
    {
        if (puntosReales.Count == 0) return;

        // Elegir posición aleatoria
        int index = Random.Range(0, puntosReales.Count);
        
        // Moverse
        transform.position = puntosReales[index].position;
    }
}