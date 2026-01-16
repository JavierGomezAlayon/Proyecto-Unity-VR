using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Necesario para acceder al efecto
using UnityEngine.InputSystem; // Necesario para leer el Joystick

public class TunelMovimiento : MonoBehaviour
{
    [Header("Referencias")]
    public Volume globalVolume; // Arrastra aquí tu Global Volume
    
    [Tooltip("La acción que usas para moverte (ej: Left Hand Move)")]
    public InputActionProperty inputMovimiento; 

    [Header("Configuración")]
    [Range(0f, 1f)]
    public float intensidadMaxima = 0.5f; // Oscuridad (0.5 suele estar bien)
    public float velocidadReaccion = 5f;  // Qué tan rápido aparece/desaparece

    // Variables internas
    private Vignette vignette;
    private float intensidadObjetivo;

    private void Start()
    {
        // Buscamos el efecto Vignette dentro del perfil del volumen
        if (globalVolume.profile.TryGet(out vignette))
        {
            vignette.active = true;
            vignette.intensity.value = 0f; // Empezamos con visión clara
        }
        else
        {
            Debug.LogError("¡No encuentro el efecto Vignette en tu Global Volume! Añádelo en el perfil.");
        }
    }

    private void Update()
    {
        if (vignette == null) return;

        // 1. LEER EL JOYSTICK
        // Leemos la fuerza del movimiento (Vector2). .magnitude nos da un valor de 0 a 1
        float fuerzaMovimiento = inputMovimiento.action.ReadValue<Vector2>().magnitude;

        // 2. DECIDIR EL OBJETIVO
        // Si mueves el stick un poquito (zona muerta de 0.1), activamos la viñeta
        if (fuerzaMovimiento > 0.1f)
        {
            intensidadObjetivo = intensidadMaxima;
        }
        else
        {
            intensidadObjetivo = 0f;
        }

        // 3. SUAVIZADO (LERP)
        // Movemos el valor actual hacia el objetivo suavemente
        float nuevoValor = Mathf.MoveTowards(vignette.intensity.value, intensidadObjetivo, velocidadReaccion * Time.deltaTime);
        vignette.intensity.Override(nuevoValor);
    }
}