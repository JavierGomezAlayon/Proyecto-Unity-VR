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

    [Header("--- SISTEMA DE ATASCO ---")]
    public float calorPorDisparo = 20f;      
    public float velocidadEnfriamiento = 5f; 
    public float umbralSoplido = 0.2f;       
    public float enfriamientoAlSoplar = 80f; 

    [Header("Visuales Atasco")]
    public ParticleSystem humoParticulas;
    
    // Variables internas
    private float calorActual = 0f;
    private bool estaAtascada = false;

    void Start()
    {
        currentBullets = maxBullets;
        audioSource = GetComponent<AudioSource>();
        
        if (movimientoJugador == null) 
            movimientoJugador = FindFirstObjectByType<Movimientopersonaje>();

        if (humoParticulas != null) humoParticulas.Stop();
    }

    private void OnEnable()
    {
        shootAction.action.performed += OnShoot;
    }

    private void OnDisable()
    {
        shootAction.action.performed -= OnShoot;
    }

    private void Update()
    {
        GestionarTemperatura();
        ActualizarHumo();
    }

    private void GestionarTemperatura()
    {
        // 1. DETECTAR SOPLIDO (Para enfriar)
        if (DetectorRuido.instancia != null)
        {
            float volumen = DetectorRuido.instancia.volumenActual;
            if (volumen > umbralSoplido)
            {
                calorActual -= enfriamientoAlSoplar * Time.deltaTime;
            }
        }

        // 2. ENFRIAMIENTO NATURAL
        calorActual -= velocidadEnfriamiento * Time.deltaTime;
        calorActual = Mathf.Clamp(calorActual, 0f, 100f);

        // 3. DESATASCAR
        // Solo se desatasca si baja a 0 totalmente
        if (estaAtascada && calorActual <= 0)
        {
            estaAtascada = false;
        }
    }

    private void ActualizarHumo()
    {
        if (humoParticulas != null)
        {
            // CAMBIO AQUÍ: El humo SOLO sale si está REALMENTE atascada.
            // Ya no sale como "advertencia" al 50%.
            bool debeHaberHumo = estaAtascada;

            if (debeHaberHumo && !humoParticulas.isPlaying) humoParticulas.Play();
            else if (!debeHaberHumo && humoParticulas.isPlaying) humoParticulas.Stop();
        }
    }

    private void OnShoot(InputAction.CallbackContext context)
    {   
        // Bloqueo total si está atascada
        if (estaAtascada)
        {
            PlaySound(emptySound);
            return;
        }

        if (currentBullets > 0)
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
        // --- LÓGICA PREDICTIVA ---
        // Comprobamos si con este disparo llegaríamos al límite (100)
        if (calorActual + calorPorDisparo >= 100f)
        {
            // SI SE VA A PASAR:
            calorActual = 100f;     // Ponemos el calor al máximo
            estaAtascada = true;    // Declaramos el atasco
            
            // Forzamos que el humo salga YA, en este mismo frame
            ActualizarHumo(); 
            
            PlaySound(emptySound);  // Suena el "click" de fallo
            
            return; // <--- IMPORTANTE: Se sale de la función AQUÍ. NO DISPARA.
        }

        // Si no se va a pasar, dispara normalmente:
        calorActual += calorPorDisparo;

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