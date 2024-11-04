using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public float lifetime = 8f;              // Time in seconds before the target disappears
    public AudioClip collisionSound;         // Sound to play on collision
    private AudioSource audioSource;         // Reference to the AudioSource component

    private void Start()
    {
        // Automatically destroy the target after a set lifetime
        Destroy(gameObject, lifetime);

        // Add an AudioSource component and assign the collision sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collisionSound;
        audioSource.playOnAwake = false; // Ensure the sound only plays on collision
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Hit detected!");
            GameManager.Instance.AddScore(scoreValue);

            // Play collision sound if available
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // Destroy the food projectile immediately
            Debug.Log("Destroying food projectile");
            Destroy(collision.gameObject);

            // Destroy the target after the sound finishes playing
            Debug.Log("Destroying target");
            Destroy(gameObject);
        }
    }
}
