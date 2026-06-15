using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public TMP_Text progressText;      
    public TMP_Text hintText;           

    [Header("Lesson Panel")]
    public GameObject lessonPanel;      
    public TMP_Text questionText;
    public Button[] answerButtons;      
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

    
    public void OpenLesson(SkillStation station)
    {
        activeStation = station;
        HideHint();

        
        SetGameplayActive(false);

        if (questionText != null)
            questionText.text = $"{station.stationName}\n\n{station.question}";

        
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

        
        SetGameplayActive(true);
    }

   
    private void SetGameplayActive(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !active;
        if (fpcMovementScript != null) fpcMovementScript.enabled = active;
    }

    // ----- WIN ---------------------------------------------------------------
    public void ShowWinScreen()
    {
        
        if (lessonPanel != null) lessonPanel.SetActive(false);
        activeStation = null;
        HideHint();

        if (winPanel != null) winPanel.SetActive(true);
        SetGameplayActive(false);   
        Time.timeScale = 0f;
    }
}
