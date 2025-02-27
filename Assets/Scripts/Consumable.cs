using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddPoints(points);
            Destroy(gameObject);
        }
    }
}