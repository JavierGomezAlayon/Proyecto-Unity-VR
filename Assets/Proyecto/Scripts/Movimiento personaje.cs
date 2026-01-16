using UnityEngine;

// Script para el movimiento del jugador
public class Movimientopersonaje : MonoBehaviour
{
    [Header("Configuración de Velocidad")]
    public float moveSpeed = 3.0f;
    public bool isMoving = true;

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Movimiento hacia adelante en su eje local Z
            transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    // Para detener el movimiento
    public void StopMovement()
    {
        isMoving = false;
    }
}