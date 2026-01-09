using UnityEngine;
using TMPro; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class RelojInteligente : MonoBehaviour
{
    [Header("Configuración UI Texto")]
    public GameObject pantallaCanvas; 
    public TextMeshProUGUI textoBalas;
    public TextMeshProUGUI textoVida;

    [Header("Configuración Barra de Vida")]
    public Image barraVidaImagen; 
    
    // Colores configurables
    public Color colorSaludAlta = new Color(1f, 0.0f, 0.8f); // Rosa
    public Color colorSaludMedia = new Color(1f, 0.5f, 0f); // Naranja
    public Color colorSaludBaja = Color.red;              // Rojo

    [Header("Configuración Game Over")]
    [Tooltip("Escribe aquí el nombre EXACTO de tu escena de Game Over")]
    public string nombreEscenaGameOver = "gameoverSceneName";

    [Header("Configuración Gesto")]
    public Transform cabezaJugador; 
    public float umbralMirada = 0.7f; 

    private void Start()
    {
        if(pantallaCanvas) pantallaCanvas.SetActive(false);
        if (cabezaJugador == null) cabezaJugador = Camera.main.transform;
        
        ActualizarVida(100);
    }

    private void Update()
    {
        DetectarGestoMirarReloj();
    }

    void DetectarGestoMirarReloj()
    {
        if (!pantallaCanvas) return;
        Vector3 direccionHaciaCabeza = (cabezaJugador.position - transform.position).normalized;
        Vector3 direccionPantalla = transform.forward; 
        float angulo = Vector3.Dot(direccionPantalla, direccionHaciaCabeza);
        bool estaMirando = angulo > umbralMirada;

        if (estaMirando != pantallaCanvas.activeSelf)
        {
            pantallaCanvas.SetActive(estaMirando);
        }
    }

    public void ActualizarBalas(int actuales, int maximas)
    {
        if(textoBalas) textoBalas.text = $"{actuales}/{maximas}";
    }

    public void ActualizarVida(int vidaActual)
    {
        if(textoVida) textoVida.text = vidaActual.ToString();

        if (barraVidaImagen)
        {
            float porcentaje = (float)vidaActual / 100f;
            barraVidaImagen.fillAmount = porcentaje;

            if (vidaActual > 50) barraVidaImagen.color = colorSaludAlta;
            else if (vidaActual > 25) barraVidaImagen.color = colorSaludMedia;
            else barraVidaImagen.color = colorSaludBaja;
        }

        // Si la vida llega a 0, cambiamos de escena
        if (vidaActual <= 0)
        {
            SceneManager.LoadScene(nombreEscenaGameOver);
        }
    }
}