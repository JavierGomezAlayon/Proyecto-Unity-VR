using UnityEngine;
using Whisper;
using Whisper.Utils;
using TMPro;

public class ControlVozVR : MonoBehaviour
{
    [Header("Referencias")]
    public SaludJugador saludJugador;
    public TextMeshProUGUI textoPantallaReloj;

    public float tiempoGrabacion = 0.8f; 
    public float tiempoRecarga = 15f;

    // Internas
    private WhisperManager whisper;
    private MicrophoneRecord microfono;
    private RelojInteligente scriptReloj;
    
    private float cronometroGrabacion = 0f;
    private float cronometroCooldown = 0f;
    
    // Estados
    private bool estaProcesando = false; 
    private bool enCooldown = false;
    private bool bloqueoMirada = false; 

    private async void Start()
    {
        whisper = GetComponent<WhisperManager>();
        microfono = GetComponent<MicrophoneRecord>();
        scriptReloj = GetComponent<RelojInteligente>();
        
        // 1. FORZAMOS CONFIGURACIÓN RÁPIDA
        whisper.language = "es"; // Español forzado
        
        // 2. PRE-CARGA DEL CEREBRO
        ActualizarPantalla("CARGANDO...");
        try { await whisper.InitModel(); } catch {}
        
        microfono.OnRecordStop += AlTerminarGrabacion;
        
        if (saludJugador == null) ActualizarPantalla("ERROR LINK");
        else ActualizarPantalla("LISTO");
    }

    private void Update()
    {
        if (scriptReloj == null || microfono == null) return;

        // FASE 1: COOLDOWN
        if (enCooldown)
        {
            cronometroCooldown -= Time.deltaTime;
            // Solo actualizamos el texto cada segundo para ahorrar recursos
            if (cronometroCooldown <= 0)
            {
                enCooldown = false;
                ActualizarPantalla("LISTO");
                bloqueoMirada = true;
            }
            else
            {
                 ActualizarPantalla($"{Mathf.CeilToInt(cronometroCooldown)}");
            }
            return;
        }

        if (estaProcesando) return;

        bool mirandoAhora = scriptReloj.elJugadorMeMira;
        if (!mirandoAhora) bloqueoMirada = false;

        // --- GRABAR (SIN TEXTO PARA IR MÁS RÁPIDO) ---
        if (mirandoAhora && !microfono.IsRecording && !bloqueoMirada)
        {
            microfono.StartRecord();
            cronometroGrabacion = 0f;
            ActualizarPantalla("🎤"); // Solo un icono, feedback instantáneo
        }

        if (microfono.IsRecording)
        {
            cronometroGrabacion += Time.deltaTime;

            // CORTE STRICTO POR TIEMPO O MIRADA
            if (cronometroGrabacion >= tiempoGrabacion || !mirandoAhora)
            {
                microfono.StopRecord(); 
                estaProcesando = true; 
                bloqueoMirada = true;
                ActualizarPantalla("⚡"); // Icono de rayo (procesando)
            }
        }
    }

    private async void AlTerminarGrabacion(AudioChunk audio)
    {
        if (whisper == null) { estaProcesando = false; return; }

        var resultado = await whisper.GetTextAsync(audio.Data, audio.Frequency, audio.Channels);
        
        if (resultado == null || string.IsNullOrEmpty(resultado.Result)) 
        {
            ActualizarPantalla("?"); // No entendió
            Invoke("VolverAListo", 0.5f);
            return;
        }

        string texto = resultado.Result.ToLower();

        // Chequeo de palabra clave
        if (texto.Contains("escudo") || texto.Contains("protec") || texto.Contains("defensa")) 
        {
            if (saludJugador != null)
            {
                // ¡ACCIÓN INMEDIATA!
                ActualizarPantalla("ESCUDO");
                saludJugador.ActivarInmunidadTemporal(5f); 
                enCooldown = true;
                cronometroCooldown = tiempoRecarga;
            }
        }
        else
        {
            ActualizarPantalla("X");
            Invoke("VolverAListo", 0.5f);
            return;
        }
        estaProcesando = false;
    }

    void VolverAListo()
    {
        estaProcesando = false;
        if (!enCooldown) ActualizarPantalla("LISTO");
    }

    void ActualizarPantalla(string mensaje)
    {
        if (textoPantallaReloj != null) textoPantallaReloj.text = mensaje;
    }
}