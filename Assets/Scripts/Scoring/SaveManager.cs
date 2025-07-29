using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public string jsonPath;
    public Save currentSave;
    public string fileName = "save.json";

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

        // Build full path to file in StreamingAssets
        jsonPath = Path.Combine(Application.streamingAssetsPath, "Saves", fileName);
        RefreshJson();
    }

    public void RefreshJson()
    {
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("File not found at path: " + jsonPath);
            return;
        }

        string jsonText = File.ReadAllText(jsonPath);
        currentSave = JsonUtility.FromJson<Save>(jsonText);
    }

    public Save GetSave()
    {
        return currentSave;
    }

    public void SaveToJSON()
    {
        string json = JsonUtility.ToJson(currentSave, true);
        File.WriteAllText(jsonPath, json);
        Debug.Log("Save updated at: " + jsonPath);
    }

    public void IncreaseAttempts()
    {
        currentSave.NewAttempt();
        SaveToJSON();
    }
}