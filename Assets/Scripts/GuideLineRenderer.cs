using UnityEngine;
using System.Collections.Generic;


/// LINE RENDERER feature.

[RequireComponent(typeof(LineRenderer))]
public class GuideLineRenderer : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Start point of the line, e.g. the teacher's desk transform.")]
    public Transform startPoint;
    [Tooltip("All stations. Leave empty to auto-find them.")]
    public List<SkillStation> stations = new List<SkillStation>();

    [Header("Line Settings")]
    public float heightOffset = 0.5f;
    public Color lineColor = new Color(0.2f, 0.8f, 1f);

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startColor = lineColor;
        line.endColor = lineColor;

        if (startPoint == null) startPoint = transform;

        if (stations.Count == 0)
            stations.AddRange(FindObjectsByType<SkillStation>());
    }

    private void Update()
    {
        SkillStation next = GetNextStation();

        if (next == null)
        {
            line.enabled = false;       // all done
            return;
        }

        line.enabled = true;
        line.SetPosition(0, startPoint.position + Vector3.up * heightOffset);
        line.SetPosition(1, next.transform.position + Vector3.up * heightOffset);
    }

    //Return the first station that is not yet completed.
    private SkillStation GetNextStation()
    {
        foreach (SkillStation s in stations)
        {
            if (s != null && !s.IsCompleted) return s;
        }
        return null;
    }
}
