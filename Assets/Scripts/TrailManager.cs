using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailManager : MonoBehaviour
{
    [Header("Trail Settings")] public float pointSpacing = 0.2f; //the gap between trails
    public float trailWidth = 0.1f;
    public GameObject trailColliderPrefab; // the prefab the check colisions

    public LineRenderer lineRenderer; // linerender draws the trail
    public List<Vector3> trailPoints = new List<Vector3>(); //all trail points
    public Vector3 lastPoint; // last trail point

    public void Start()
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

        // Fallback material in case none is set
        if (lineRenderer.material == null)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }

// start the trail at the player location
        AddPoint(transform.position);
    }

    public void Update()
    {
        // adds to the trail if the player has moved far enough
        if (Vector3.Distance(transform.position, lastPoint) >= pointSpacing)
        {
            AddPoint(transform.position);
        }
    }

    /// <summary>
    /// Adds a new point to the trail and spawns a trail collider at that point.
    /// </summary>
    public void AddPoint(Vector3 point)
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
            piece.tag = "Trail"; // checks tag for collision
            piece.layer = gameObject.layer;
            piece.transform.localScale = new Vector3(trailWidth, trailWidth, 1f);

            // give the trail an identify 
            TrailIdentity identity = piece.AddComponent<TrailIdentity>();
            identity.owner = this.gameObject;
        }
    }

    /// <summary>
    /// Updates the visual color of the trail.
    /// </summary>
    public void SetTrailColor(Color color)
    {
        // checks in linerender exists
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            // sets the start and end colour of the trail
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







