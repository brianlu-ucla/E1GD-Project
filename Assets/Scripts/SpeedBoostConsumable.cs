using UnityEngine;

public class SpeedBoostConsumable : MonoBehaviour
{
    public float speedMultiplier = 2f;  
    public float boostDuration = 10f;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.BoostSpeed(speedMultiplier, boostDuration);
            }

            Destroy(gameObject);
        }
    }
}
