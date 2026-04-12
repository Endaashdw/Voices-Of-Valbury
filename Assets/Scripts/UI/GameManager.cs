using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Paused, //add functionality if needed
        GameOver
    }

    public GameState currentState;
    public GameState previousState;
    public bool isGameOver = false;

    [Header("Screens")]
    public GameObject gameScreen;
    public GameObject resultScreen;

    [Header("Results")]
    public TMP_Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
        }

        DisableScreens(); //in case screens are open
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                ScoreManager.instance.AddToScore(1);
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Debug.Log("GAME OVER");
                    Time.timeScale = 0f;
                    DisplayResults();
                }
                break;
            default:
                Debug.LogError("Game state does not exist");
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    void DisableScreens()
    {
        resultScreen.SetActive(false);
    }

    public void GameOver()
    {
        scoreText.text = ScoreManager.instance.GetScore().ToString();
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        gameScreen.SetActive(false);
        resultScreen.SetActive(true);
    }
}
