using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [Tooltip("Exact name of the gameplay scene file, e.g. EmpowerEd")]
    public string gameSceneName = "EmpowerEd";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
