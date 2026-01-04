using UnityEngine;

public class RecargaGesto : MonoBehaviour
{
    public Disparo disparoScript;

    [Header("Configuración")]
    [Tooltip("¿Cuánto hay que inclinar? 1.0 es totalmente vertical, 0.5 es 45 grados.")]
    [Range(0.1f, 1.0f)]
    public float anguloNecesario = 0.7f; 

    [Tooltip("Tiempo para evitar que recargue mil veces por segundo")]
    public float tiempoEntreRecargas = 1.0f;
    
    private float ultimoTiempoRecarga;

    void Update()
    {
        // 1. Averiguamos hacia dónde apunta la pistola
        // Vector3.down es el suelo del mundo (0, -1, 0)
        // transform.forward es la flecha azul de tu pistola
        
        // El "Producto Punto" (Dot) nos dice si dos direcciones coinciden.
        // Si es 1, miran igual. Si es -1, miran opuesto.
        float inclinacion = Vector3.Dot(transform.forward, Vector3.down);

        // 2. Comprobamos si miramos al suelo
        // Si 'inclinacion' es mayor que el umbral (ej: 0.7), es que estás apuntando abajo
        if (inclinacion > anguloNecesario)
        {
            // Verificamos el tiempo para no spamear
            if (Time.time > ultimoTiempoRecarga + tiempoEntreRecargas)
            {
                // Intentamos recargar
                // (El script de Disparo ya se encarga de no recargar si está lleno)
                disparoScript.Reload();
                
                ultimoTiempoRecarga = Time.time;
            }
        }
    }
}