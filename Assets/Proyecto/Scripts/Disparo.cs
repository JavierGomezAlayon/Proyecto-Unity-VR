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
    private int currentBullets;
    private int maxBullets = 10;
    private AudioSource audioSource;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;

    public GameObject collisionParticleEffect;

    void Start()
    {
        currentBullets = maxBullets;
        audioSource = GetComponent<AudioSource>();
    }

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
        if ( currentBullets > 0)
        {
            ShootBullet();
        }
        else
        {
            // PlaySound(emptySound);
        }
        
    }

    private void ShootBullet()
    {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = spawnPoint.forward * bulletSpeed;
            }
            Destroy(bullet, 10f);
            currentBullets--;
            // PlaySound(shootSound);
    }

    public void Reload()
    {
        if (currentBullets < maxBullets)
        {
            currentBullets = maxBullets;
            // PlaySound(reloadSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
