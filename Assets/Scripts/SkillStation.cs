using UnityEngine;


public class SkillStation : MonoBehaviour
{
    [Header("Station Info")]
    public string stationName = "Budgeting";

    [Header("Lesson Content")]
    [TextArea] public string question = "You get paid. What should you do FIRST?";
    public string[] answers = { "Spend it all", "Save some, then budget", "Lend it away" };
    [Tooltip("Index of the correct answer in the array above.")]
    public int correctIndex = 1;
    [TextArea] public string teachingNote = "Saving first builds stability. This is the foundation of budgeting.";

    [Header("Visual Feedback")]
    public GameObject completedMarker;   
    public Renderer stationRenderer;     
    public Color completedColor = Color.green;

    [Header("Optional Audio (beyond-module extra)")]
    public AudioSource correctSound;

    public bool IsCompleted { get; private set; }

    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        if (completedMarker != null) completedMarker.SetActive(false);
    }

    
    public void OpenLesson()
    {
        if (IsCompleted) return;
     
        if (uiManager != null) uiManager.OpenLesson(this);
    }

    
    public void SubmitAnswer(int chosenIndex)
    {
        if (chosenIndex == correctIndex)
        {
            MarkComplete();
            uiManager.ShowFeedback(true, teachingNote);
        }
        else
        {
            uiManager.ShowFeedback(false, "Not quite. Try again - think long term.");
        }
    }

    private void MarkComplete()
    {
        IsCompleted = true;

        if (completedMarker != null) completedMarker.SetActive(true);
        if (stationRenderer != null) stationRenderer.material.color = completedColor;
        if (correctSound != null) correctSound.Play();

        GameManager.Instance.CompleteStation();
    }
}
