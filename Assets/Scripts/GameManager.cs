using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;           // Reference to ScoreText UI element
    public TextMeshProUGUI timerText;           // Reference to TimerText UI element
    public TextMeshProUGUI gameOverText;        // Reference to GameOverText UI element
    public AudioClip scoreIncreaseSound;        // Sound to play when score increases
    public AudioClip scoreDecreaseSound;        // Sound to play when score decreases
    public AudioClip gameOverSound;             // Sound to play when the game ends
    private AudioSource audioSource;            // Reference to the AudioSource component

    private int score = 0;                      // Player's score
    public float gameTime = 60f;                // Total game time in seconds
    public bool isGameOver = false;             // Flag to indicate game over

    private void Awake()
    {
        // Set up singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize UI elements
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.RoundToInt(gameTime);
        gameOverText.gameObject.SetActive(false); // Hide Game Over text at the start

        // Add an AudioSource component and assign the game over sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Prevent sound from playing on start
    }

    private void Update()
    {
        // Only update the timer if the game is not over
        if (!isGameOver)
        {
            UpdateTimer();
        }
    }

    // Method to update the timer
    private void UpdateTimer()
    {
        gameTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.RoundToInt(gameTime);

        if (gameTime <= 0)
        {
            EndGame();
        }
    }

    // Method to add or subtract score
    public void AddScore(int value)
    {
        // Determine if the score is increasing or decreasing
        if (value > 0)
        {
            PlaySound(scoreIncreaseSound); // Play the increase sound if score goes up
        }
        else if (value < 0)
        {
            PlaySound(scoreDecreaseSound); // Play the decrease sound if score goes down
        }

        // Update score and display it
        score += value;
        if (score < 0) score = 0;  // Optional: Prevent the score from going below 0
        scoreText.text = "Score: " + score;
    }

    // Method to end the game
    private void EndGame()
    {
        isGameOver = true;  // Set game-over flag to true
        gameOverText.gameObject.SetActive(true); // Show Game Over text
        timerText.text = "Time: 0"; // Set timer to 0

        // Play the game-over sound if available
        PlaySound(gameOverSound);
    }

    // Method to play a sound
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Public method to check if the game is over
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
