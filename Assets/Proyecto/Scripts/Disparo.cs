using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty shootAction;

    [Header("Referencias Externas")]
    public RelojInteligente relojRef;
    public Movimientopersonaje movimientoJugador;

    [Header("Bala")]
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 50f;
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
        
        if (movimientoJugador == null) 
            movimientoJugador = FindFirstObjectByType<Movimientopersonaje>();
    }

    private void OnEnable()
    {
        shootAction.action.performed += OnShoot;
    }

    private void OnDisable()
    {
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
            PlaySound(emptySound);
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            float velocidadFinal = bulletSpeed;

            if (movimientoJugador != null && movimientoJugador.isMoving)
            {
                velocidadFinal += movimientoJugador.moveSpeed;
            }

            rb.linearVelocity = spawnPoint.forward * velocidadFinal; 
        }
        Destroy(bullet, 10f);
        currentBullets--;
        if(relojRef) relojRef.ActualizarBalas(currentBullets, maxBullets);
        PlaySound(shootSound);
    }
    
    public void Reload()
    {
        if (currentBullets < maxBullets)
        {
            currentBullets = maxBullets;
            PlaySound(reloadSound);
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