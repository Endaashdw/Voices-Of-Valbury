using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private float energy;
    public static EnergyManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //scoreText.text = "Energy: " + energy;
    }

    public void SetEnergy(float value)
    {
        energy = value;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu") // Replace with your actual scene name
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        instance = null;
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public float GetEnergy()
    {
        return energy;
    }
}
