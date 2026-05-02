using UnityEngine;

public class DaggerDamage : MonoBehaviour
{
    public int damage = 1;
    public float speed = 6f;
    public Vector2 direction = Vector2.left;
    public float lifeTime = 5f;
    public bool destroyOnHit = true;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

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
