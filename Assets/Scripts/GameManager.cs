using UnityEngine;
using UnityEngine.SceneManagement;


/// Central brain of the simulation. Tracks how many skill stations the player
/// has completed and decides when the player has "graduated" (win condition).
/// Other scripts talk to this single instance.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [Tooltip("Total number of skill stations in the room.")]
    public int totalStations = 4;

    [Header("Runtime State")]
    public int stationsCompleted = 0;

    [SerializeField] private UIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        // Make sure the game runs at normal speed (in case a previous win paused it).
        Time.timeScale = 1f;
        uiManager?.UpdateProgress(stationsCompleted, totalStations);
    }

    //Called by a SkillStation when its lesson is finished correctly.
    public void CompleteStation()
    {
        stationsCompleted++;
        uiManager?.UpdateProgress(stationsCompleted, totalStations);

        if (stationsCompleted >= totalStations)
            uiManager?.ShowWinScreen();
    }

    //Hooked to the "Restart" button on the win screen.
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMainMenu()
   {
       Time.timeScale = 1f;
       SceneManager.LoadScene("MainMenu");
   }
}
