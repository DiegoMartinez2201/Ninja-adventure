using UnityEngine;

public class DaggerSpawner : MonoBehaviour
{
    public GameObject daggerPrefab;
    public float spawnInterval = 2f;
    public float daggerSpeed = 6f;
    public float daggerRotationOffset = -90f;
    public bool useRandomYOffset = false;
    public Vector2 randomYOffset = new Vector2(-1f, 1f);

    [Header("Referencia al Objetivo")]
    public Transform playerTarget; // Arrastra al Ninja aquí en el Inspector

    private float nextSpawnTime;

    private void Awake()
    {
        FindPlayerTarget();
    }

    private void Update()
    {
        if (playerTarget == null)
        {
            FindPlayerTarget();
        }

        if (daggerPrefab == null || Time.time < nextSpawnTime || playerTarget == null)
        {
            return;
        }

        SpawnDagger();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void SpawnDagger()
    {
        Vector3 spawnPosition = transform.position;
        if (useRandomYOffset)
        {
            spawnPosition.y += Random.Range(randomYOffset.x, randomYOffset.y);
        }

        Vector2 direction = (playerTarget.position - spawnPosition).normalized;
        if (direction == Vector2.zero)
        {
            return;
        }

        // La imagen de la daga apunta hacia arriba, por eso usamos un offset de -90 grados.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, angle + daggerRotationOffset);

        GameObject newDagger = Instantiate(daggerPrefab, spawnPosition, spawnRotation);

        DaggerDamage daggerDamage = newDagger.GetComponent<DaggerDamage>();
        if (daggerDamage != null)
        {
            daggerDamage.Initialize(direction, daggerSpeed, playerTarget);
        }
    }

    private void FindPlayerTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTarget = player.transform;
        }
    }
}