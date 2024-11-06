using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  
    public TextMeshProUGUI highScoreText; 
    private int score = 0;  
    private int highScore = 0;   

    void Start()
    {
        highScore = PlayerPrefs.GetInt("Record", 0); // Load saved high score, default 0
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount; // Increment score every kill
        UpdateScoreText();

        // Do a check to see if new high score has been reached
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("Record", highScore); // Save the new high score
            UpdateHighScoreText();
        }
    }

    //Update the current score
    private void UpdateScoreText()
    {
        scoreText.text = "Souls Collected: " + score;
    }
    //Update high score
    private void UpdateHighScoreText()
    {
        highScoreText.text = "Record: " + highScore;
    }
}
