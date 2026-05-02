using UnityEngine;

public class DaggerSpawner : MonoBehaviour
{
    public GameObject daggerPrefab;
    public float spawnInterval = 2f;
    public Vector2 randomYOffset = new Vector2(-1f, 1f);

    [Header("Referencia al Objetivo")]
    public Transform playerTarget; // Arrastra al Ninja aquí en el Inspector

    private float nextSpawnTime;

    private void Update()
    {
        if (daggerPrefab == null || Time.time < nextSpawnTime || playerTarget == null)
        {
            return;
        }

        SpawnDagger();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void SpawnDagger()
    {
        // 1. Posición de salida con el offset aleatorio
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += Random.Range(randomYOffset.x, randomYOffset.y);

        // 2. Calcular dirección hacia el Ninja
        Vector2 direction = (playerTarget.position - spawnPosition).normalized;

        // 3. Calcular el ángulo de rotación para que "mire" al Ninja
        // Usamos Atan2 para obtener el ángulo en radianes y lo pasamos a grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, angle);

        // 4. Instanciar la daga
        GameObject newDagger = Instantiate(daggerPrefab, spawnPosition, spawnRotation);

        // 5. IMPORTANTE: Le damos velocidad en esa dirección
        // Asumiendo que tu daga tiene Rigidbody2D y un script de movimiento
        Rigidbody2D rb = newDagger.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float speed = 5f; // Puedes cambiar esta velocidad o usar una del prefab
            rb.linearVelocity = direction * speed;
        }
    }
}