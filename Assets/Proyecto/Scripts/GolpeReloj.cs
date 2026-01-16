using UnityEngine;
using TMPro;

public class GolpeReloj : MonoBehaviour
{
    [Header("Referencias")]
    public SaludJugador saludJugador;     // Para activar el escudo
    public TextMeshProUGUI textoEstado;

    [Header("Configuración")]
    public string tagManoGolpeadora = "ManoIzquierda";
    public float duracionEscudo = 5f;
    public float tiempoRecarga = 15f;

    // Variables internas
    private bool enCooldown = false;
    private float cronometro = 0f;

    private void Start()
    {
        ActualizarTexto("LISTO\n(Golpea)");
    }

    private void Update()
    {
        // Solo gestionamos la cuenta atrás del cooldown
        if (enCooldown)
        {
            cronometro -= Time.deltaTime;
            
            // Mostramos el tiempo restante (sin decimales)
            ActualizarTexto($"{Mathf.CeilToInt(cronometro)}s");

            if (cronometro <= 0)
            {
                enCooldown = false;
                ActualizarTexto("LISTO\n(Golpea)");
            }
        }
    }

    // --- DETECCIÓN DE GOLPE FÍSICO ---
    private void OnTriggerEnter(Collider other)
    {
        // 1. Si estamos recargando, ignoramos el golpe
        if (enCooldown) return;

        // 2. Comprobamos si lo que ha tocado el reloj es la mano derecha
        if (other.CompareTag(tagManoGolpeadora))
        {
            ActivarHabilidad();
        }
    }

    void ActivarHabilidad()
    {
        if (saludJugador != null)
        {
            Debug.Log(">>> ¡GOLPE DETECTADO! ESCUDO ACTIVO <<<");
            
            // Llamamos a tu script de salud
            saludJugador.ActivarInmunidadTemporal(duracionEscudo);
            
            // Iniciamos el cooldown
            enCooldown = true;
            cronometro = tiempoRecarga;
            
            ActualizarTexto("ESCUDO\nACTIVO");
        }
    }

    void ActualizarTexto(string mensaje)
    {
        if (textoEstado != null) textoEstado.text = mensaje;
    }
}