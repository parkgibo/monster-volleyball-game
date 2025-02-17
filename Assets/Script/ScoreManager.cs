using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public int aiScore = 0;
    public Text playerScoreText;
    public Text aiScoreText;
    public void AddScore(int playerPoints, int aiPoints)
    {
        playerScore += playerPoints;
        aiScore += aiPoints;
        UpdateScoreUI();

        if (playerScore >= 10)
        {
            Debug.Log("ÇÃ·¹ÀÌ¾î ½Â¸®!");
            ResetGame();
        }
        else if (aiScore >= 10)
        {
            Debug.Log("AI ½Â¸®!");
            ResetGame();
        }
    }
    void UpdateScoreUI()
    {
        playerScoreText.text = "Player: " + playerScore;
        aiScoreText.text = "AI: " + aiScore;
    }
    void ResetGame()
    {
        playerScore = 0;
        aiScore = 0;
        UpdateScoreUI();
    }
}
