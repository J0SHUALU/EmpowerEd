using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Attached to the Mini First Person Controller's player (or its camera).
/// The player WALKS around the classroom using the Mini FPC, and this script
/// fires a RAYCAST forward from the camera to detect which skill station the
/// player is looking at. Clicking (new Input System) opens that station.
///
/// This mirrors the PlayerInteraction approach from DarkroomEscape: a forward
/// camera ray plus a click to interact.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast")]
    public Camera cam;                  // the FPC camera
    public float interactRange = 4f;
    public LayerMask stationMask;       // layer the stations live on

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

    /// <summary>Cast a ray from screen center forward and remember what we hit.</summary>
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

    /// <summary>
    /// New Input System callback for the "Interact" action (left click / E / tap).
    /// Wire this on the PlayerInput component (Invoke Unity Events).
    /// </summary>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        currentStation?.OpenLesson();
    }
}
