using UnityEngine;

public class FoodProjectile : MonoBehaviour
{
    public float lifetime = 3f; // Time in seconds before the food projectile disappears

    private void Start()
    {
        // Automatically destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

}
