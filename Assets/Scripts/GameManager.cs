using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;
    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! Score: " + score);
    }
}