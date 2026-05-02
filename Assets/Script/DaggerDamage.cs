using UnityEngine;

public class DaggerDamage : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;
    public bool destroyOnHit = true;

    private void Start()
    {
        // Ya no necesitamos moverla aquí con Translate.
        // El Spawner ya le dio una velocidad inicial al Rigidbody2D.
        Destroy(gameObject, lifeTime);
    }

    // BORRAMOS EL MÉTODO UPDATE que tenía el Translate
    // porque el Rigidbody2D se encarga del movimiento de forma más real.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamagePlayer(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamagePlayer(collision.gameObject);
    }

    private void DamagePlayer(GameObject objectHit)
    {
        if (!objectHit.CompareTag("Player"))
        {
            return;
        }

        PlayerHealth playerHealth = objectHit.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}