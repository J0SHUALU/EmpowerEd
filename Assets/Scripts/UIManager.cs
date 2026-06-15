using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles ALL on-screen UI: the hover hint, the progress counter, the lesson
/// popup (question + answer buttons), the feedback message, and the win screen.
///
/// The lesson popup reuses a fixed set of answer buttons; we relabel them for
/// whatever station is open. Each button calls OnAnswerButton with its index.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public TMP_Text progressText;       // "Skills Learned: 2 / 4"
    public TMP_Text hintText;           // "Click to start: Budgeting"

    [Header("Lesson Panel")]
    public GameObject lessonPanel;      // parent panel, hidden at start
    public TMP_Text questionText;
    public Button[] answerButtons;      // assign 3 buttons in the Inspector
    public TMP_Text feedbackText;

    [Header("Win Screen")]
    public GameObject winPanel;

    [Header("First Person Controller")]
    [Tooltip("Drag the Mini FPC player root here so we can pause look/move during a lesson.")]
    public MonoBehaviour fpcMovementScript;   // the Mini FPC's movement component

    // The station currently being taught.
    private SkillStation activeStation;

    private void Start()
    {
        if (lessonPanel != null) lessonPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        HideHint();
        if (feedbackText != null) feedbackText.text = "";
    }

    // ----- HUD ---------------------------------------------------------------
    public void UpdateProgress(int done, int total)
    {
        if (progressText != null)
            progressText.text = $"Skills Learned: {done} / {total}";
    }

    public void ShowHint(string msg)
    {
        if (hintText == null) return;
        hintText.text = msg;
        hintText.gameObject.SetActive(true);
    }

    public void HideHint()
    {
        if (hintText != null) hintText.gameObject.SetActive(false);
    }

    // ----- LESSON ------------------------------------------------------------

    /// <summary>Open the lesson popup for a given station.</summary>
    public void OpenLesson(SkillStation station)
    {
        activeStation = station;
        HideHint();

        // Free the cursor and pause the FPC so the player can click answers.
        SetGameplayActive(false);

        if (questionText != null)
            questionText.text = $"{station.stationName}\n\n{station.question}";

        // Relabel each answer button for this station's answers.
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < station.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = station.answers[i];
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        if (feedbackText != null) feedbackText.text = "";
        if (lessonPanel != null) lessonPanel.SetActive(true);
    }

    /// <summary>
    /// Hooked to each answer button's OnClick in the Inspector.
    /// Pass the button's index (0,1,2) via the inspector event field.
    /// </summary>
    public void OnAnswerButton(int index)
    {
        if (activeStation != null)
            activeStation.SubmitAnswer(index);
    }

    public void ShowFeedback(bool correct, string message)
    {
        if (feedbackText != null)
        {
            feedbackText.color = correct ? Color.green : Color.red;
            feedbackText.text = message;
        }

        // If correct, close the lesson after a short delay.
        if (correct) Invoke(nameof(CloseLesson), 1.5f);
    }

    public void CloseLesson()
    {
        if (lessonPanel != null) lessonPanel.SetActive(false);
        activeStation = null;

        // Re-lock the cursor and resume the FPC for walking around.
        SetGameplayActive(true);
    }

    /// <summary>
    /// Toggles between "walking around" mode and "reading a lesson" mode.
    /// When false: cursor is free, FPC paused (so mouse can click buttons).
    /// When true: cursor locked, FPC active (so the player can look/move).
    /// </summary>
    private void SetGameplayActive(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !active;
        if (fpcMovementScript != null) fpcMovementScript.enabled = active;
    }

    // ----- WIN ---------------------------------------------------------------
    public void ShowWinScreen()
    {
        // Close the lesson panel without re-locking the cursor (we want it free here).
        if (lessonPanel != null) lessonPanel.SetActive(false);
        activeStation = null;
        HideHint();

        if (winPanel != null) winPanel.SetActive(true);
        SetGameplayActive(false);   // free cursor for the restart button
        Time.timeScale = 0f;
    }
}
