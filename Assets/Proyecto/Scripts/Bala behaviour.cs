using UnityEngine;

public class Balabehaviour : MonoBehaviour
{
    public float damage = 10f;
    public GameObject collisionParticleEffect;

    private void OnCollisionEnter(Collision collision)
    {
        // 1. COMPROBAR SI ES UN ENEMIGO NORMAL
        EnemyLife enemy = collision.gameObject.GetComponent<EnemyLife>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Impacto físico detectado en enemigo");
            ParticulasImpacto();
            Destroy(gameObject);
            return;
        }

        // 2. COMPROBAR SI ES EL BOSS (¡ESTO FALTABA!)
        BossLife boss = collision.gameObject.GetComponent<BossLife>();
        if (boss != null)
        {
            boss.TakeDamage(damage); // Le quitamos vida al Boss
            Debug.Log("Impacto físico detectado en BOSS");
            ParticulasImpacto();
            Destroy(gameObject);
            return;
        }

        // 3. COMPROBAR SI ES UN TRIGGER DE CAMBIO DE ESCENA
        if (collision.gameObject.CompareTag("AlDañarEnviarA"))
        {
            AlDañarEnviarA damageTrigger = collision.gameObject.GetComponent<AlDañarEnviarA>();
            if (damageTrigger != null)
            {
                damageTrigger.TakeDamage(damage);
                ParticulasImpacto();
                Destroy(gameObject);
                return;
            }
        }

        // 4. COMPROBAR SI ES ENTORNO (PAREDES, SUELO)
        if (collision.gameObject.CompareTag("Entorno"))
        {
            ParticulasImpacto();
        }
    }

    private void ParticulasImpacto()
    {
        if (collisionParticleEffect != null)
        {
            Instantiate(collisionParticleEffect, transform.position, Quaternion.identity);
        }
    }
}