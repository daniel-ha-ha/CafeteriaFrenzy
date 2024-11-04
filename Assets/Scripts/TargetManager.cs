using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targets;        // Array for regular target prefabs
    public GameObject penaltyTarget;    // Prefab for the penalty target
    public float spawnInterval = 2f;
    public float penaltySpawnChance = 0.2f;  // 20% chance to spawn a penalty target

    public Transform leftBoundary;
    public Transform rightBoundary;
    public Transform topBoundary;
    public Transform bottomBoundary;

    private void Start()
    {
        InvokeRepeating("SpawnTarget", 1f, spawnInterval);  // Start spawning targets at regular intervals
    }

    void SpawnTarget()
    {
        if (GameManager.Instance.IsGameOver())
        {
            CancelInvoke("SpawnTarget"); // Stop further invocations of SpawnTarget
            return;
        }
        float randomChance = Random.value;

        GameObject targetToSpawn;
        if (randomChance < penaltySpawnChance)
        {
            // Spawn a penalty target
            targetToSpawn = penaltyTarget;
        }
        else
        {
            // Spawn a regular target
            int index = Random.Range(0, targets.Length);
            targetToSpawn = targets[index];
        }

        // Generate a random position within the boundaries
        float randomX = Random.Range(leftBoundary.position.x, rightBoundary.position.x);
        float randomY = Random.Range(bottomBoundary.position.y, topBoundary.position.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        Instantiate(targetToSpawn, spawnPosition, Quaternion.identity);
    }

}
