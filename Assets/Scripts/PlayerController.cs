using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite idleSprite;         // Sprite for idle/resting
    public Sprite throwingSprite;     // Sprite for throwing action
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite to idle
        spriteRenderer.sprite = idleSprite;
    }
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.sprite = throwingSprite;
            StartCoroutine(SwitchBackToIdle());
        }
    }

    private IEnumerator SwitchBackToIdle()
    {
        // Wait for a short time (e.g., 0.2 seconds) to show the throw sprite
        yield return new WaitForSeconds(0.1f);

        // Switch back to idle sprite
        spriteRenderer.sprite = idleSprite;
    }
}
