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
    public Button[] buttons;
    public GameObject[] saveRelatedObjects;
    public TMP_InputField textInputField;

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
    }

    public void SetButtons(bool value)
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(value);
        }
    }

    public void SaveScore()
    {
        Debug.Log("Saving score of " + ScoreManager.instance.GetScore());
        SaveManager.instance.GetSave().UpdateScoreList(ScoreManager.instance.GetScore(), textInputField.text);
        SaveManager.instance.SaveToJSON();
        SetButtons(true);
    }
}
