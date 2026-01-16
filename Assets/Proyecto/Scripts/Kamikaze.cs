using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Kamikaze : MonoBehaviour
{
    private Transform objetivo; 

    [Header("Configuración")]
    public float salud = 30f;
    public float distanciaParaExplotar = 2.5f;
    public float radioExplosion = 5.0f;
    public float daño = 30f;

    [Header("Efectos")]
    public GameObject efectoExplosionPrefab;
    public AudioClip sonidoExplosion;

    private NavMeshAgent agente;
    private bool haExplotado = false;
    private float tiempoDeNacimiento;

    void Start()
    {
        tiempoDeNacimiento = Time.time;
        agente = GetComponent<NavMeshAgent>();

        if (agente != null)
        {
            agente.enabled = false;
            agente.enabled = true;
        }

        GameObject jugadorEncontrado = GameObject.FindGameObjectWithTag("Player");
        if (jugadorEncontrado != null) objetivo = jugadorEncontrado.transform;
    }

    void Update()
    {
        if (haExplotado || objetivo == null) return;

        agente.SetDestination(objetivo.position);

        // Comprobar distancia para explotar
        if (!agente.pathPending && agente.remainingDistance <= distanciaParaExplotar)
        {
            Detonar();
        }
    }

    public void TakeDamage(float amount)
    {
        if (haExplotado) return;
        salud -= amount;
        if (salud <= 0) Detonar(); // Aquí también llamamos a Detonar
    }

    public void Detonar()
    {
        // Seguridad: No explotar en el primer segundo de vida (para no spawnear matando)
        if (Time.time < tiempoDeNacimiento + 1.0f) return;

        if (haExplotado) return;
        haExplotado = true;
        
        if(agente != null) agente.isStopped = true;

        EnemyLife miVida = GetComponent<EnemyLife>();
        if (miVida != null)
        {
            // Le hacemos daño infinito para forzar la muerte
            miVida.TakeDamage(99999f); 
        }
        // -----------------------------

        // Efectos
        if (efectoExplosionPrefab != null) Instantiate(efectoExplosionPrefab, transform.position, Quaternion.identity);
        if (sonidoExplosion != null) AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position);

        // Daño
        Collider[] objetosGolpeados = Physics.OverlapSphere(transform.position, radioExplosion);
        List<GameObject> yaGolpeados = new List<GameObject>();

        foreach (Collider col in objetosGolpeados)
        {
            SaludJugador vidaJugador = col.GetComponentInParent<SaludJugador>();
            if (vidaJugador != null && !yaGolpeados.Contains(vidaJugador.gameObject))
            {
                vidaJugador.RecibirDaño((int)daño);
                yaGolpeados.Add(vidaJugador.gameObject);
            }

            // Fuego amigo
            if (col.gameObject != this.gameObject)
            {
                EnemyLife vidaEnemigo = col.GetComponent<EnemyLife>();
                if (vidaEnemigo != null && !yaGolpeados.Contains(vidaEnemigo.gameObject))
                {
                    vidaEnemigo.TakeDamage(daño);
                    yaGolpeados.Add(vidaEnemigo.gameObject);
                }
            }
        }

        Destroy(gameObject);
    }
}