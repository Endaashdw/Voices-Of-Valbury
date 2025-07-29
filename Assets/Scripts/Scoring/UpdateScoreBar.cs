using System.Text;
using TMPro;
using UnityEngine;

public class UpdateScoreBar : MonoBehaviour
{
    public SaveManager reader;
    [Header("TMP Text Fields")]
    public TMP_Text scoreText;
    public TMP_Text attemptsText;

    void Start()
    {
        Save save = reader.GetSave();

        attemptsText.text = "TOTAL ATTEMPTS: " + save.attempts;

        // Build top 10 leaderboard string
        StringBuilder sb = new();
        sb.AppendLine("TOP 10 HIGHSCORES:");

        for (int i = 0; i < save.topScores.Count; i++)
        {
            ScoreEntry entry = save.topScores[i];
            sb.AppendLine($"{entry.playerName} - {entry.score}");
        }

        for (int i = save.topScores.Count; i < 10; i++)
        {
            sb.AppendLine("---");
        }

        scoreText.text = sb.ToString();
    }
}
