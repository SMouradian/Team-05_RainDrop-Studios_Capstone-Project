using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    [SerializeField] private float spawnInterval = 3f; // Time between spawns
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-5f, -5f); // Minimum spawn coordinates
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(5f, 5f); // Maximum spawn coordinates

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true) // Loop indefinitely for continuous spawning
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval

            // Generate a random position within the defined spawn area
            Vector3 randomSpawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f // Assuming 2D or a fixed Z-position in 3D
            );

            if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            }
        }
    }
}