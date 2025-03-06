using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHearts = 5;           // How many hearts the player starts with (also the number of lives)
    public float currentHealth;         // Current health measured in half-heart units (each heart = 2 units)
    
    [Header("UI Elements")]
    // An array of UI Image components representing each heart (set up in the Canvas)
    public Image[] heartImages;
    // Sprites for the three states: full, half, and empty
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [Header("Respawn Settings")]
    // The spawn point where the player should respawn after dying
    public Transform respawnPoint;
    
    void Start()
    {
        // Initialize current health to full (each heart equals 2 health units)
        currentHealth = maxHearts * 2;
        transform.position = respawnPoint.position;
        UpdateHeartsUI();
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            // Treat falling off the map as fatal damage.
            HandleDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
        else
        {
            UpdateHeartsUI();
        }
    }

    // Handles the player "dying" (losing a heart and respawning)
    void HandleDeath()
    {
        // Remove one heart (life) permanently
        maxHearts = Mathf.Max(0, maxHearts - 1);

        if (maxHearts > 0)
        {
            // Reset current health to full based on the new max hearts
            currentHealth = maxHearts * 2;
            UpdateHeartsUI();
            Respawn();
        }
        else
        {
            // All hearts are gone – game over logic goes here.
            Debug.Log("Game Over");
            // Optionally disable the player, show a game over screen, etc.
        }
    }

    // Respawns the player at the designated spawn point.
    void Respawn()
    {
        transform.position = respawnPoint.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // Updates the UI to reflect the current health.
    void UpdateHeartsUI()
    {
        // Loop through each heart UI image.
        // (Assumes heartImages.Length is at least as many as the maximum hearts.)
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < maxHearts)
            {
                // Each heart represents 2 health units.
                if (currentHealth >= (i + 1) * 2)
                {
                    heartImages[i].sprite = fullHeart;
                }
                else if (currentHealth == (i * 2) + 1)
                {
                    heartImages[i].sprite = halfHeart;
                }
                else
                {
                    heartImages[i].sprite = emptyHeart;
                }
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
}
