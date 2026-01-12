using UnityEngine;
using Whisper;
using Whisper.Utils;

[RequireComponent(typeof(WhisperManager))]   // Obliga a que existan estos componentes
[RequireComponent(typeof(MicrophoneRecord))]
[RequireComponent(typeof(RelojInteligente))]
public class ControlVozVR : MonoBehaviour
{
    [Header("Conexión Automática")]
    // Estos ya no hace falta arrastrarlos, se llenan solos en el Start
    private WhisperManager whisper;
    private MicrophoneRecord microfono;
    private RelojInteligente scriptReloj;
    
    [Header("Conexión Externa (Arrastrar Jugador)")]
    public SaludJugador saludJugador; // Este SÍ arrástralo manual (tu XR Origin)

    private bool estabaMirando = false;

    private void Start()
    {
        // AUTOCONEXIÓN: Buscamos los componentes en este mismo objeto (El Reloj)
        whisper = GetComponent<WhisperManager>();
        microfono = GetComponent<MicrophoneRecord>();
        scriptReloj = GetComponent<RelojInteligente>();

        // Nos suscribimos al evento de Whisper
        microfono.OnRecordStop += AlTerminarGrabacion;
    }

    private void Update()
    {
        // Si por algún motivo falló la conexión, no hacemos nada
        if (scriptReloj == null || microfono == null) return;

        // Leemos la variable pública del otro script
        bool mirandoAhora = scriptReloj.elJugadorMeMira;

        // --- LÓGICA DE ACTIVACIÓN ---
        
        // 1. Al empezar a mirar -> GRABAR
        if (mirandoAhora && !estabaMirando)
        {
            if (!microfono.IsRecording)
            {
                microfono.StartRecord();
                Debug.Log("Reloj: Escuchando voz...");
            }
        }

        // 2. Al dejar de mirar -> PROCESAR
        if (!mirandoAhora && estabaMirando)
        {
            if (microfono.IsRecording)
            {
                microfono.StopRecord();
                Debug.Log("Reloj: Procesando...");
            }
        }

        estabaMirando = mirandoAhora;
    }

    private async void AlTerminarGrabacion(AudioChunk audio)
    {
        var resultado = await whisper.GetTextAsync(audio.Data, audio.Frequency, audio.Channels);
        
        if (resultado == null || string.IsNullOrEmpty(resultado.Result)) 
            return;

        string texto = resultado.Result.ToLower();
        Debug.Log("Whisper oyó: " + texto);

        // Palabras clave
        if (texto.Contains("escudo") || texto.Contains("protec") || texto.Contains("defensa")) 
        {
            if (saludJugador != null)
            {
                saludJugador.ActivarInmunidadTemporal(5f);
            }
        }
    }
}