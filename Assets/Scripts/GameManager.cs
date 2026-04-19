using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;

    [Header("Score")]
    private int coins = 0;
    private float timeElapsed = 0f;

    [Header("UI - HUD")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeText;

    [Header("UI - Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalCoinsText;
    public TextMeshProUGUI finalTimeText;

    [Header("Audio")]
    public AudioSource backgroundMusic; 

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        // Acumula el tiempo
        timeElapsed += Time.deltaTime;

        // Actualiza el HUD
        coinsText.text = "Monedas: " + coins;
        timeText.text = "Tiempo: " + Mathf.FloorToInt(timeElapsed) + "s";
    }

    public void AddScore(int amount)
    {
        coins += amount;
    }

    public void GameOver()
    {
        isGameOver = true;
        backgroundMusic.Stop();
        gameOverPanel.SetActive(true);
        finalCoinsText.text = "Monedas: " + coins;
        finalTimeText.text = "Tiempo: " + Mathf.FloorToInt(timeElapsed) + "s";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); 
    }
}