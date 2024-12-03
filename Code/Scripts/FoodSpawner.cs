using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public BaseFood[] foodPrefabs;       // Array of food prefabs
    public Vector3 spawnAreaMin;         // Minimum spawn area boundary
    public Vector3 spawnAreaMax;         // Maximum spawn area boundary
    public float spawnCheckRadius = 0.5f; // Radius to check for collision
    public int maxSpawnAttempts = 10;   // Maximum retries to find a valid spawn location
    public LayerMask spawnLayerMask;    // Layer mask to filter objects for collision checks

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
        {
            // Generate a random spawn position
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                0.5f, // Fixed height
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // Check for overlaps with other objects
            if (IsSpawnLocationValid(spawnPosition))
            {
                // Select a food prefab based on rarity
                BaseFood selectedFood = SelectFoodByRarity();
                if (selectedFood != null)
                {
                    Instantiate(selectedFood.gameObject, spawnPosition, Quaternion.identity);
                }
                return; // Spawn successful, exit the method
            }
        }

        Debug.LogWarning("Failed to spawn food after maximum attempts.");
    }

    private bool IsSpawnLocationValid(Vector3 position)
    {
        // Check for overlapping colliders at the spawn position
        Collider[] colliders = Physics.OverlapSphere(position, spawnCheckRadius, spawnLayerMask);
        return colliders.Length == 0; // Valid if no objects overlap
    }

    private BaseFood SelectFoodByRarity()
    {
        // Calculate total weight
        int totalWeight = 0;
        foreach (var food in foodPrefabs)
        {
            totalWeight += food.rarity;
        }

        // Get a random value within the total weight
        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Select the food based on the weighted random value
        foreach (var food in foodPrefabs)
        {
            cumulativeWeight += food.rarity;
            if (randomValue < cumulativeWeight)
            {
                return food;
            }
        }

        return null; // Fallback (should never happen if weights are set correctly)
    }
}
