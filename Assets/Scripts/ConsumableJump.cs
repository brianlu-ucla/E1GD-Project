using UnityEngine;

public class PermanentExtraJumpDashConsumable : MonoBehaviour
{
    public int extraJumps = 1; 
    public int extraDashes = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.AddExtraJumps(extraJumps);
                playerMovement.AddExtraDashes(extraDashes);
            }
            Destroy(gameObject);
        }
    }
}