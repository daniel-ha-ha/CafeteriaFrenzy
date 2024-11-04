using UnityEngine;

public class FoodThrower : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float spinForce = 50f;
    public AudioClip throwSound;  // Sound to play when food is thrown
    private AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        // Add an AudioSource component and assign the throw sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = throwSound;
        audioSource.playOnAwake = false;  // Prevent the sound from playing on start

        // Set the volume to a higher level to make the sound louder
        audioSource.volume = 10f;  
    }

    void Update()
    {
        // Only allow throwing if the game is not over
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameOver())
        {
            ThrowFood();
        }
    }

    void ThrowFood()
    {
        // Play the throw sound at the specified volume
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Instantiate the food projectile
        GameObject food = Instantiate(foodPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = food.GetComponent<Rigidbody2D>();

        // Apply forward throw force and torque for spinning effect
        rb.AddForce(transform.up * throwForce, ForceMode2D.Impulse);
        rb.AddTorque(spinForce);
    }
}
