using UnityEngine;
using TMPro; // Necesario para el texto

public class RelojInteligente : MonoBehaviour
{
    [Header("Configuración UI")]
    public GameObject pantallaCanvas; // Arrastra aquí el 'CanvasPantalla'
    public TextMeshProUGUI textoBalas;
    public TextMeshProUGUI textoVida;

    [Header("Configuración Gesto")]
    public Transform cabezaJugador; // Arrastra la Main Camera aquí
    [Tooltip("Que tan preciso debe ser el giro de muñeca (1 = perfecto, 0.5 = 45 grados)")]
    public float umbralMirada = 0.7f; 

    private void Start()
    {
        // Al principio apagamos la pantalla para ahorrar batería virtual ;)
        if(pantallaCanvas) pantallaCanvas.SetActive(false);
        
        // Si no asignaste la cámara manual, la busca sola
        if (cabezaJugador == null) cabezaJugador = Camera.main.transform;
    }

    private void Update()
    {
        DetectarGestoMirarReloj();
    }

    void DetectarGestoMirarReloj()
    {
        if (!pantallaCanvas) return;

        // 1. Calculamos la dirección desde el reloj hacia la cabeza
        Vector3 direccionHaciaCabeza = (cabezaJugador.position - transform.position).normalized;

        // 2. Calculamos hacia dónde mira la "cara" del reloj
        Vector3 direccionPantalla = transform.forward; 

        // 3. Producto Punto: Compara que tan paralelos son dos vectores.
        // 1.0 significa que la pantalla te mira directo a los ojos.
        float angulo = Vector3.Dot(direccionPantalla, direccionHaciaCabeza);

        // Si el ángulo supera el umbral, encendemos la pantalla
        bool estaMirando = angulo > umbralMirada;

        if (estaMirando != pantallaCanvas.activeSelf)
        {
            pantallaCanvas.SetActive(estaMirando);
        }
    }

    // Métodos públicos para que el arma los llame
    public void ActualizarBalas(int actuales, int maximas)
    {
        if(textoBalas) textoBalas.text = $"{actuales}/{maximas}";
    }

    public void ActualizarVida(int vida)
    {
        if(textoVida) textoVida.text = $"HP: {vida}";
    }
}