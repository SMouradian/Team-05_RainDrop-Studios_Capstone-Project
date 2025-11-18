using UnityEngine;

public class EnemyStackSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;      // drag your enemy prefab here
    public Transform spawnPoint;        // where the stack starts
    public int stackCount = 5;          // how many enemies in the stack
    public float verticalOffset = 1f;   // space between each enemy (Y axis)

    [Header("Input Settings")]
    public KeyCode spawnKey = KeyCode.E; // key to press to spawn

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnStack();
        }
    }

    void SpawnStack()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("EnemyStackSpawner: Missing enemyPrefab or spawnPoint.");
            return;
        }

        for (int i = 0; i < stackCount; i++)
        {
            Vector3 spawnPos = spawnPoint.position + new Vector3(0f, i * verticalOffset, 0f);
            Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);
        }
    }
}
