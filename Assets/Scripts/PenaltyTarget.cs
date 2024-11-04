using UnityEngine;

public class PenaltyTarget : MonoBehaviour
{
    public int penaltyValue = -10;      // Points to deduct when hit
    public float lifetime = 3f;         // Time in seconds before the penalty target disappears
    public AudioClip collisionSound;    // Sound to play on collision
    private AudioSource audioSource;    // Reference to the AudioSource component

    private void Start()
    {
        // Automatically destroy the penalty target after a set lifetime
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
            // Deduct points by calling GameManager
            GameManager.Instance.AddScore(penaltyValue);

            // Play collision sound if available
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // Destroy the food projectile immediately
            Destroy(collision.gameObject); // Destroy the food projectile

            // Destroy the penalty target after the sound finishes playing
            Destroy(gameObject); // Delayed destroy to allow sound to play
        }
    }
}
