using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneChange(string name)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }
}
