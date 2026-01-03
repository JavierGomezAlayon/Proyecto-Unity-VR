using UnityEngine;
using UnityEngine.InputSystem; // Importante para el nuevo Input System

public class RecargaGesto : MonoBehaviour
{
    public Disparo disparoScript;
    public InputActionProperty angularVelocityProperty; // Asiganmeros la velocidad angular del mando
    public float sensibilidadRecarga = 10f; // Sensibilidad para detectar el gesto de recarga
    public float tiempoEntreRecargas = 30f; // Tiempo mínimo entre recargas
    private float ultimoTiempoRecarga;

    void OnEnable()
    {
        angularVelocityProperty.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocidadGiro = angularVelocityProperty.action.ReadValue<Vector3>();
        float fuerzaGiro = velocidadGiro.magnitude;

        if (fuerzaGiro > sensibilidadRecarga && Time.time > ultimoTiempoRecarga + tiempoEntreRecargas)
        {
            // Realizar la recarga
            disparoScript.Reload();
            ultimoTiempoRecarga = Time.time;
        }
        
    }
}
