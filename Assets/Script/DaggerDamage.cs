using UnityEngine;

public class DaggerDamage : MonoBehaviour
{
    public int damage = 1;
    public float speed = 6f;
    public Vector2 direction = Vector2.left;
    public float maxLifeTime = 30f;
    public float destroyDistanceAfterPassingPlayer = 0.5f;
    public bool destroyOnHit = true;

    private Transform targetToPass;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void Update()
    {
        Vector2 moveDirection = direction.normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (targetToPass == null)
        {
            return;
        }

        Vector2 targetToDagger = (Vector2)transform.position - (Vector2)targetToPass.position;
        if (Vector2.Dot(targetToDagger, moveDirection) > destroyDistanceAfterPassingPlayer)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 newDirection, float newSpeed, Transform playerTarget)
    {
        direction = newDirection.normalized;
        speed = newSpeed;
        targetToPass = playerTarget;
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