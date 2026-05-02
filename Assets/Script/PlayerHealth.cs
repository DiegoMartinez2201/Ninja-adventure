using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invulnerabilityTime = 1f;

    private int currentHealth;
    private float nextDamageTime;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time < nextDamageTime)
        {
            return;
        }

        currentHealth -= damage;
        nextDamageTime = Time.time + invulnerabilityTime;

        Debug.Log("Vida del jugador: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador murio");
        gameObject.SetActive(false);
    }
}
