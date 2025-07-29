using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int attempts;
    public List<ScoreEntry> topScores = new();

    public bool CheckForHighScore(int newScore)
    {
        if (topScores.Count < 10) return true;
        foreach (ScoreEntry entry in topScores)
        {
            if (newScore > entry.score)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateScoreList(int newScore, string newName)
    {
        topScores.Add(new ScoreEntry { score = newScore, playerName = newName });

        topScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (topScores.Count > 10)
        {
            topScores.RemoveRange(10, topScores.Count - 10);
        }
    }


    public int HighestScore()
    {
        return topScores.Count > 0 ? topScores[0].score : 0;
    }

    public ScoreEntry HighestScoreEntry()
    {
        return topScores.Count > 0 ? topScores[0] : null;
    }

    public void NewAttempt()
    {
        attempts++;
    }
}

