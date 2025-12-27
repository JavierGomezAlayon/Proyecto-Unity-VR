using UnityEngine;
using UnityEngine.InputSystem; // Importante para el nuevo Input System

public class Disparo : MonoBehaviour
{
    [Header("Input")]
    // Esto nos permitirá seleccionar el gatillo desde el Inspector
    public InputActionProperty shootAction;

    [Header("Bala")]
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 20f;
    

    public GameObject collisionParticleEffect;

    private void OnEnable()
    {
        // Nos suscribimos al evento de "presionado"
        shootAction.action.performed += OnShoot;
    }

    private void OnDisable()
    {
        // Desuscribirse para evitar errores de memoria
        shootAction.action.performed -= OnShoot;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = spawnPoint.forward * bulletSpeed;
        }
        Destroy(bullet, 10f);
    }
}
