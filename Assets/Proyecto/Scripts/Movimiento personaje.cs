using UnityEngine;

public class Movimientopersonaje : MonoBehaviour
{
    [Header("Configuración de Velocidad")]
    public float moveSpeed = 3.0f; // Velocidad constante (ajusta a tu gusto)
    public bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            // Movimiento hacia adelante en su eje local Z
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
    
    // Para detener el movimiento
    public void StopMovement()
    {
        isMoving = false;
    }
}