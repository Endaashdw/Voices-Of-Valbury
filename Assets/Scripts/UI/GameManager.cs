using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject[] retryRelatedObjects;
    public GameObject[] saveRelatedObjects;
    public TMP_InputField textInputField;

    [Header("Timer")]
    public float timeLimit;
    public float timer = 0f;
    public bool timerReady = false;
    public TMP_Text timerText;
    public SceneController controller;
    public string sceneName;

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

                if (isGameOver && timerReady)
                {
                    timer += Time.unscaledDeltaTime;

                    float remaining = Mathf.Max(timeLimit - timer, 0f);
                    timerText.text = $"Returning in: {remaining:F1}s"; 

                    if (timer > timeLimit)
                    {
                        controller.SceneChange(sceneName);
                    }
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
        SetButtons(false);
        SetSaveObjects(false);
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

        Debug.Log(ScoreManager.instance.GetScore() + " VS " + SaveManager.instance.GetSave().HighestScore());
        if (!SaveManager.instance.GetSave().CheckForHighScore(ScoreManager.instance.GetScore()))
        {
            SetButtons(true);
        }
        else
        {
            SetSaveObjects(true);
        }
    }

    public void SetSaveObjects(bool value)
    {
        foreach (GameObject saveRelatedObject in saveRelatedObjects)
        {
            saveRelatedObject.SetActive(value);
        }
        timerReady = false;
    }

    public void SetButtons(bool value)
    {
        foreach (GameObject retryRelatedObject in retryRelatedObjects)
        {
            retryRelatedObject.SetActive(value);
        }
        timerReady = true;
    }

    public void SaveScore()
    {
        Debug.Log("Saving score of " + ScoreManager.instance.GetScore());
        SaveManager.instance.GetSave().UpdateScoreList(ScoreManager.instance.GetScore(), textInputField.text);
        SaveManager.instance.SaveToJSON();
        SetButtons(true);
    }
}
