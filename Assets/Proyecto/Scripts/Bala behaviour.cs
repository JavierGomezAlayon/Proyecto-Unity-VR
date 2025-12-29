using UnityEngine;

public class Balabehaviour : MonoBehaviour
{
    public float damage = 10f; // Daño que inflige la bala
    public GameObject collisionParticleEffect; // Particula de impacto

    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si el objeto impactado tiene el componente EnemyLife
        EnemyLife enemy = collision.gameObject.GetComponent<EnemyLife>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Impacto físico detectado en enemigo");
            ParticulasImpacto();
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Entorno"))
        {
            ParticulasImpacto();
            Destroy(gameObject);
        }

        // Destruimos la bala al impactar contra cualquier cosa y opcionalmente instanciamos un efecto de partículas
        
    }

    private void ParticulasImpacto()
    {
        if (collisionParticleEffect != null)
        {
            Instantiate(collisionParticleEffect, transform.position, Quaternion.identity);
        }
    }
}
