using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text jumpText;
    public TMP_Text dashText;

    private int jumps = 999;
    private int dashes = 999;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void UpdateJumps(int jumps)
    {
        this.jumps = jumps;
        UpdateJumpUI();
    }

    public void UpdateDashes(int dashes)
    {
        this.dashes = dashes;
        UpdateDashUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateJumpUI()
    {
        if (jumpText != null)
        {
            jumpText.text = "Jumps: " + jumps;
        }
    }

    private void UpdateDashUI()
    {
        if (dashText != null)
        {
            dashText.text = "Dashes: " + dashes;
        }
    }
}
