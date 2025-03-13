using UnityEngine;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text jumpText;
    public TMP_Text dashText;

    private int jumps = 999;
    private int dashes = 999;

    private int levelStartingScore = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelStartingScore(int s)
    {
        levelStartingScore = s;
    }

    public void ResetScoreToLevelStart()
    {
        score = levelStartingScore;
        UpdateScoreUI();
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

    public void SwitchLevel()
    {
        /*PlayerMovement pm = FindFirstObjectByType<PlayerMovement>();
        if (pm != null)
        {
            int remainingJumps = pm.MaxJumps - pm.JumpCount;
            int remainingDashes = pm.MaxDashes - pm.DashCount;
            int bonus = (remainingJumps + remainingDashes) * 100;
            AddPoints(bonus);
        }*/
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)  % SceneManager.sceneCountInBuildSettings);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded: {scene.name}, Current Score: {score}");

        TMP_Text foundScoreText = GameObject.Find("scoreText")?.GetComponent<TMP_Text>();
        TMP_Text foundJumpText = GameObject.Find("jumpText")?.GetComponent<TMP_Text>();
        TMP_Text foundDashText = GameObject.Find("dashText")?.GetComponent<TMP_Text>();
        Debug.Log($"Found ScoreText? {(foundScoreText != null)}");
        Debug.Log($"Found JumpText? {(foundJumpText != null)}");
        Debug.Log($"Found DashText? {(foundDashText != null)}");

        if (foundScoreText != null) { scoreText = foundScoreText; }
        if (foundJumpText != null) { jumpText = foundJumpText; }
        if (foundDashText != null) { dashText = foundDashText; }

        UpdateScoreUI();
        UpdateJumpUI();
        UpdateDashUI();

        SetLevelStartingScore(score);
    }
}
