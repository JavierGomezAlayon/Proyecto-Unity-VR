using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [Header("Configuración")]
    public float rangoAtaque = 15f;

    private Transform playerTransform;
    private Animator animator;
    private EnemyLife vidaScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        vidaScript = GetComponent<EnemyLife>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("LookAtPlayer: No se encontró al Player.");
        }
    }

    void Update()
    {
        // 1. Si no hay jugador o el enemigo ha muerto, no hacemos nada
        if (playerTransform == null || (vidaScript != null && vidaScript.health <= 0)) 
            return;

        // 2. Calculamos la distancia
        float distancia = Vector3.Distance(transform.position, playerTransform.position);

        // 3. Lógica de animación y rotación
        if (distancia <= rangoAtaque)
        {
            // ESTAMOS EN RANGO: Apuntamos y activamos animación de ataque
            if(animator) animator.SetBool("Atacando", true);
            
            // Hacemos que gire suavemente hacia el jugador (más realista que LookAt directo)
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            direction.y = 0; // Para que no se incline hacia arriba/abajo, solo rote en Y
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // FUERA DE RANGO: Volvemos a Idle
            if(animator) animator.SetBool("Atacando", false);
        }
    }
}