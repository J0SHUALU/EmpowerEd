using UnityEngine;

/// <summary>
/// One skill station in the classroom (e.g. Budgeting, Job/CV, Hygiene,
/// Reading). When clicked, it asks the UIManager to open its lesson: a short
/// question with multiple choice answers. When answered correctly it marks
/// itself complete and tells the GameManager.
///
/// COLLISION: the station has a collider so the raycast can hit it, and a
/// larger trigger collider used to detect proximity for the guide line.
/// </summary>
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
    public GameObject completedMarker;   // optional: a green light/checkmark
    public Renderer stationRenderer;     // optional: to tint when done
    public Color completedColor = Color.green;

    [Header("Optional Audio (beyond-module extra)")]
    public AudioSource correctSound;

    public bool IsCompleted { get; private set; }

    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (completedMarker != null) completedMarker.SetActive(false);
    }

    /// <summary>Called by StationRaycaster when the player clicks this station.</summary>
    public void OpenLesson()
    {
        if (IsCompleted) return;
        // Hand this station's lesson to the UI to display.
        uiManager.OpenLesson(this);
    }

    /// <summary>Called by the UI when the player picks an answer.</summary>
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
