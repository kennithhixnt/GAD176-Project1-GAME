using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailManager : MonoBehaviour
{
    [Header("Prefab Reference")]
    public GameObject trailSegmentPrefab;

    private LineRenderer lineRenderer;
    private List<Vector3> trailPoints = new List<Vector3>();
    private float updateDistance = 0.1f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Call this once at spawn to initialize the trail
    public void StartTrail(Vector3 startPos)
    {
        trailPoints.Clear();
        trailPoints.Add(startPos);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);
    }

    // Call this every frame to update the trail
    public void UpdateTrail(Vector3 currentPos)
    {
        // Safeguard: If trail was never started, initialize it
        if (trailPoints.Count == 0)
        {
            StartTrail(currentPos);
            return;
        }

        Vector3 lastPoint = trailPoints[trailPoints.Count - 1];

        if (Vector3.Distance(lastPoint, currentPos) > updateDistance)
        {
            trailPoints.Add(currentPos);

            lineRenderer.positionCount = trailPoints.Count;
            lineRenderer.SetPositions(trailPoints.ToArray());

            CreateSegment(lastPoint, currentPos);
        }
    }

    // Spawns a single trail segment between two points
    private void CreateSegment(Vector3 from, Vector3 to)
    {
        Vector3 pos = (from + to) / 2f;
        Vector3 dir = to - from;
        float distance = dir.magnitude;

        GameObject segment = Instantiate(trailSegmentPrefab, pos, Quaternion.identity);
        segment.transform.up = dir.normalized;
        segment.transform.localScale = new Vector3(0.1f, distance, 1f);

        // Set the owner on the segment
        TrailSegment ts = segment.GetComponent<TrailSegment>();
        if (ts != null)
        {
            ts.owner = this.gameObject;
        }

        // Match the color of the segment to the LineRenderer
        SpriteRenderer sr = segment.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color color = Color.white;
            if (lineRenderer.material.HasProperty("_Color"))
                color = lineRenderer.material.color;
            sr.color = color;
        }
    }
}




