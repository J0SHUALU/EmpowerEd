using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast")]
    public Camera cam;                  // the FPC camera
    public float interactRange = 4f;
    public LayerMask stationMask;      

    [Header("UI")]
    public UIManager uiManager;

    // The station currently in the player's crosshair (or null).
    private SkillStation currentStation;

    private void Reset()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        DetectStation();
    }

    //Cast a ray from screen center forward and remember what was hit.
    private void DetectStation()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, stationMask))
        {
            SkillStation station = hit.collider.GetComponent<SkillStation>();
            if (station != null && !station.IsCompleted)
            {
                currentStation = station;
                uiManager?.ShowHint($"Click to start: {station.stationName}");
                return;
            }
        }

        currentStation = null;
        uiManager?.HideHint();
    }

    
    /// New Input System callback for the "Interact" action (left click / E / tap).
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        currentStation?.OpenLesson();
    }
}
