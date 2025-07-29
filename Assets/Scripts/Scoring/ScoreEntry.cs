[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;

    public bool CheckForHighScore(int newScore)
    {
        if (newScore > score)
        {
            return true;
        }
        return false;
    }

    public void UpdateScore(int newScore, string newString)
    {
        if (CheckForHighScore(newScore))
        {
            score = newScore;
            playerName = newString;
        }
    }
}