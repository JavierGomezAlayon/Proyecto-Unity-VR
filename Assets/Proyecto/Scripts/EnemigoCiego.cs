using UnityEngine;
using UnityEngine.AI; // Necesario para moverse

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyLife))] // Obliga a tener tu script de vida
public class IA_Suicida : MonoBehaviour
{
    [Header("Sentidos (Oído)")]
    public float rangoAudicion = 15f; // Distancia a la que te puede oír
    [Range(0.01f, 1f)]
    public float umbralRuido = 0.2f;  // Sensibilidad (ajústalo según tu micro)

    [Header("Explosión")]
    public float distanciaParaExplotar = 2.5f;
    public int dañoAlJugador = 30;    // Cuánta vida quita (Entero, como pide tu script)
    public float radioExplosion = 4f; // Qué tan lejos llega el daño
    public GameObject efectoVisualExplosion; // Arrastra aquí un sistema de partículas

    // Variables internas
    private NavMeshAgent agente;
    private Transform jugador;
    private EnemyLife miVida; // Referencia a TU script EnemyLife
    private bool detectado = false;
    private bool haExplotado = false;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        miVida = GetComponent<EnemyLife>();

        // Buscamos al jugador por la etiqueta "Player"
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null) 
        {
            jugador = objJugador.transform;
        }
        else
        {
            Debug.LogError("¡Falta el Tag 'Player' en el XR Origin!");
        }
    }

    private void Update()
    {
        // Si no hay jugador o ya exploté, no hago nada
        if (jugador == null || haExplotado) return;

        // Si el enemigo muere por disparos antes de explotar, paramos la IA
        if (miVida.health <= 0) 
        {
            agente.isStopped = true;
            this.enabled = false; // Apagamos este script
            return;
        }

        float distancia = Vector3.Distance(transform.position, jugador.position);

        // --- FASE 1: ESCUCHA ---
        if (!detectado)
        {
            // Chequeamos si el DetectorRuido existe y si el volumen supera el umbral
            if (DetectorRuido.instancia != null)
            {
                // Si estás cerca Y haces ruido
                if (distancia < rangoAudicion && DetectorRuido.instancia.volumenActual > umbralRuido)
                {
                    ActivarModoPersecucion();
                }
            }
        }

        // --- FASE 2: PERSECUCIÓN ---
        if (detectado)
        {
            agente.SetDestination(jugador.position);

            // --- FASE 3: DETONACIÓN ---
            if (distancia <= distanciaParaExplotar)
            {
                Explotar();
            }
        }
    }

    void ActivarModoPersecucion()
    {
        detectado = true;
        
        // Feedback visual: Se pone rojo para avisar que te oyó
        var renderer = GetComponentInChildren<Renderer>();
        if (renderer != null) renderer.material.color = Color.red;

        // Aumentamos velocidad por la adrenalina
        agente.speed *= 1.5f; 
    }

    void Explotar()
    {
        haExplotado = true;
        agente.isStopped = true; // Frenar en seco

        // 1. Instanciar partículas de explosión
        if (efectoVisualExplosion != null)
        {
            Instantiate(efectoVisualExplosion, transform.position, Quaternion.identity);
        }

        // 2. Buscar al jugador y hacer daño (CONECTADO A TU SCRIPT SALUDJUGADOR)
        Collider[] afectados = Physics.OverlapSphere(transform.position, radioExplosion);
        foreach (Collider col in afectados)
        {
            if (col.CompareTag("Player"))
            {
                // Buscamos tu script SaludJugador
                SaludJugador saludScript = col.GetComponent<SaludJugador>();
                
                // Si no está en el objeto principal, buscamos en los padres (común en VR)
                if (saludScript == null) saludScript = col.GetComponentInParent<SaludJugador>();

                if (saludScript != null)
                {
                    // ¡Aquí llamamos a tu función!
                    saludScript.RecibirDaño(dañoAlJugador);
                    Debug.Log("BOOM! Jugador dañado.");
                }
            }
        }

        // 3. Auto-destrucción del enemigo (Usando tu script EnemyLife)
        // Ocultamos el modelo visual para que desaparezca instantáneamente
        foreach(var ren in GetComponentsInChildren<Renderer>()) ren.enabled = false;
        GetComponent<Collider>().enabled = false; // Desactivar collider para no estorbar

        // Le hacemos daño infinito al enemigo para que EnemyLife registre la baja y avise al Manager
        miVida.TakeDamage(9999f);
    }

    // Dibujamos los rangos en el editor para que sea fácil ajustar
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAudicion); // Radio de escucha
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaParaExplotar); // Radio de explosión
    }
}