using UnityEngine;
using TMPro; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

// Script de comportamiento del reloj
public class RelojInteligente : MonoBehaviour
{
    [Header("Configuración UI Texto")]
    public GameObject pantallaCanvas; 
    public TextMeshProUGUI textoBalas;
    public TextMeshProUGUI textoVida;

    [Header("Configuración Barra de Vida")]
    public Image barraVidaImagen; 
    
    public Color colorSaludAlta = new Color(1f, 0.0f, 0.8f);
    public Color colorSaludMedia = new Color(1f, 0.5f, 0f);
    public Color colorSaludBaja = Color.red;

    [Header("Configuración Game Over")]
    public string nombreEscenaGameOver = "GameOver";

    [Header("Configuración Gesto")]
    public Transform cabezaJugador; 
    public float umbralMirada = 0.7f; 

    public bool elJugadorMeMira { get; private set; } 
    // -------------------------------------------------------

    private void Start()
    {
        if(pantallaCanvas) pantallaCanvas.SetActive(false);
        if (cabezaJugador == null && Camera.main != null) 
            cabezaJugador = Camera.main.transform;
        
        ActualizarVida(100);
    }

    private void Update()
    {
        DetectarGestoMirarReloj();
    }

    void DetectarGestoMirarReloj()
    {
        if (!pantallaCanvas) return;
        
        // Calculamos si miras
        Vector3 direccionHaciaCabeza = (cabezaJugador.position - transform.position).normalized;
        Vector3 direccionPantalla = transform.forward; 
        float angulo = Vector3.Dot(direccionPantalla, direccionHaciaCabeza);
        
        // Guardamos el dato en la variable pública
        elJugadorMeMira = angulo > umbralMirada;

        // Encendemos/Apagamos pantalla
        if (elJugadorMeMira != pantallaCanvas.activeSelf)
        {
            pantallaCanvas.SetActive(elJugadorMeMira);
        }
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

        if (vidaActual <= 0)
        {
            SceneManager.LoadScene(nombreEscenaGameOver);
        }
    }
}