using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailManager : MonoBehaviour
{
    [Header("Prefab Reference")]
    public GameObject trailSegmentPrefab;
    [Header("Trail Settings")]
    public float pointSpacing = 0.2f;
    public float trailWidth = 0.1f;
    public GameObject trailColliderPrefab;

    private LineRenderer lineRenderer;
    private List<Vector3> trailPoints = new List<Vector3>();
    private float updateDistance = 0.1f;
    private Vector3 lastPoint;

    void Awake()
    {
        // Get LineRenderer
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer missing from GameObject.");
            return;
        }

        // Setup
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = trailWidth;
        lineRenderer.endWidth = trailWidth;

        if (lineRenderer.material == null)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }

        AddPoint(transform.position);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, lastPoint) >= pointSpacing)
        {
            AddPoint(transform.position);
        }
    }

    private void AddPoint(Vector3 point)
    {
        // Update trail visuals
        trailPoints.Add(point);
        lineRenderer.positionCount = trailPoints.Count;
        lineRenderer.SetPosition(trailPoints.Count - 1, point);
        lastPoint = point;

        // Spawn a collider object for trail collision
        if (trailColliderPrefab != null)
        {
            GameObject piece = Instantiate(trailColliderPrefab, point, Quaternion.identity);
            piece.tag = "Trail"; // must match tag check in collision
            piece.layer = gameObject.layer; // optional: match player's layer
            piece.transform.localScale = new Vector3(trailWidth, trailWidth, 1f);

            // Attach trail identity so we know who made this trail
            TrailIdentity identity = piece.AddComponent<TrailIdentity>();
            identity.owner = this.gameObject;
        }
    }

    public void SetTrailColor(Color color)
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            if (lineRenderer.material != null)
            {
                lineRenderer.material.color = color;
            }
        }
        else
        {
            Debug.LogError("LineRenderer missing when trying to set trail color.");
        }
    }
}







