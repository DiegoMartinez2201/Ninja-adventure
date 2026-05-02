using UnityEngine;

public class DaggerSpawner : MonoBehaviour
{
    public GameObject daggerPrefab;
    public float spawnInterval = 2f;
    public Vector2 randomYOffset = new Vector2(-1f, 1f);

    private float nextSpawnTime;

    private void Update()
    {
        if (daggerPrefab == null || Time.time < nextSpawnTime)
        {
            return;
        }

        SpawnDagger();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void SpawnDagger()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += Random.Range(randomYOffset.x, randomYOffset.y);

        Instantiate(daggerPrefab, spawnPosition, transform.rotation);
    }
}
