using System;
using System.Collections.Generic;
using UnityEngine;

// Tutorial videos for rope mechanics I used:
// https://youtu.be/iGlD3f-5JpA?si=fDHJhXLPd620-b9V
// https://youtu.be/P-UscoFwaE4?si=cLlJbDLvHPjRXKZV

// The rope is more for visuals, the players are actually restrained by

[DefaultExecutionOrder(-4)]
public class Rope : MonoBehaviourSingleton<Rope>
{
    // Event triggered when the IsAttached state of the rope changes (e.g., when it is IsAttached or detachd).
    public event EventHandler OnAttachedStateChanged;

    [SerializeField] private GameObject ropeSegmentPrefab;  // Prefab for newVelocity single segment of the rope.
    [SerializeField] private int segmentsToSpawn = 10;         
    [SerializeField] private LineRenderer lineRenderer;     // LineRenderer component to visualize the rope's appearance.

    private List<Transform> ropeSegmentsList = new(); 

    [Header("Points")]
    [SerializeField] private BaloonController baloon;       // Reference to the balloon (start point of the rope).
    [SerializeField] private Rigidbody2D player;            // Reference to the player (end point of the rope).

    [SerializeField] private Transform startPoint;          // Transfrm repesnting the start point of the rope.
    [SerializeField] private Transform endPoint;            // Transform representing the end point of the rope.

    [Header("Balloon Distance Limiter")]
    [SerializeField] private float springForce = 10f;
    [SerializeField] private float maxDistance = 5f;

    [Header("Wall detection")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float playerMaxForce = 3f;
    [SerializeField] private float untiedLaunchBonus = 0.5f;

    public bool IsAttached { get; private set; }

    private void Awake()
    {
        // Generate the rope when the game starts.
        CheckRope(IsAttached);
    }

    private void Start()
    {
        baloon.OnChangedSize += Baloon_OnChangedSize;
    }

    private void Baloon_OnChangedSize(object sender, EventArgs e)
    {
        if (IsAttached)
        {
            //// Re-generate the rope if it's IsAttached.
            CheckRope(!IsAttached);
        }

        // If isn't attached, rope already was destroyed
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        // Check for input to isAttached/detach the rope.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckRope(IsAttached);

            // Trigger the event to notify listeners of the change in attachment state.
            OnAttachedStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    #region BALLON DISTANCE LIMITED
    void FixedUpdate()
    {
        // Calculate the distance between the two objects
        Vector3 toConnected = player.transform.position - baloon.transform.position;
        float distance = toConnected.magnitude;

        if (distance > maxDistance)
        {
            // Calculate the spring force
            Vector3 force = toConnected.normalized * (distance - maxDistance) * springForce;

            // Apply the force to this object only
            baloon.Rb.AddForce(force);
        }
    }
    #endregion

    private void LateUpdate()
    {
        // Update the LineRenderer's positions to match the rope segments' positions.
        UpdateLineRenderer();
    }

    #region SEGMENTED ROPE
    // Generates the rope by creating and connecting segments between the start and end points.
    // If true deataches, if false attaches
    public void CheckRope(bool isAttached)
    {
        // Clear any existing rope segments
        DestroyPreviousRope();

        if (isAttached)
        {
            IsAttached = false; // Toggle the IsAttached state.

            //DestroyPreviousRope(); // Destroy the rope if it's detached.
            DestroyLineRenderer(); // Disable the LineRenderer when the rope is detached.
            return;
        }

        // Determine the direction and distance of the raycast
        Vector2 direction = endPoint.position - startPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(startPoint.position, direction.normalized, direction.magnitude, wallLayer);

        if (hit.collider != null)
        {
            return;
        }

        IsAttached = true; // Toggle the IsAttached state.

        // Create the rope segments from the start point to the hit point
        GenerateRopePhysics();

        // Create the LineRenderer and update its positions
        CreateLineRenderer();
        UpdateLineRenderer();
    }

    void GenerateRopePhysics()
    {
        ropeSegmentsList = new();
        Rigidbody2D previousRB = baloon.Rb;          // Start by connecting to the balloon's Rigidbody2D.

        // Create each rope segment and link it to the previous one.
        for (int i = 0; i < segmentsToSpawn; i++)
        {
            // Determine the position of this segment based on its index.
            Vector2 segmentPosition = Vector2.Lerp(startPoint.position, endPoint.position, (float)i / (segmentsToSpawn - 1));

            // Instantiate the segment and set its parent to this object.
            var newSegment = Instantiate(ropeSegmentPrefab.transform, segmentPosition, Quaternion.identity, transform);
            Rigidbody2D currentRB = newSegment.GetComponent<Rigidbody2D>();

            // Attach this segment to the previous one using newVelocity HingeJoint2D.
            HingeJoint2D hingeJoint = newSegment.GetComponent<HingeJoint2D>();
            hingeJoint.connectedBody = previousRB;

            ropeSegmentsList.Add(newSegment);
            previousRB = currentRB;       // Update the reference for the next iteration.
        }

        // Connect the last segment to the player's Rigidbody2D.
        HingeJoint2D endHinge = ropeSegmentsList[ropeSegmentsList.Count - 1].gameObject.AddComponent<HingeJoint2D>();
        endHinge.connectedBody = player.GetComponent<Rigidbody2D>();
    }

    // Destroys the rope segments when the rope is detached.
    void DestroyPreviousRope()
    {
        if (ropeSegmentsList != null)
        {
            // Iterate through all segments and disable/destroy them.
            foreach (var ropeSegment in ropeSegmentsList)
            {
                if (ropeSegment != null)
                {
                    ropeSegment.gameObject.SetActive(false);    // Disable the segment's GameObject.
                    Destroy(ropeSegment.gameObject, 0.2f);       // Destroy the GameObject after newVelocity short delay.
                }
            }
        }
    }
    #endregion

    #region LINE RENDERER
    private void DestroyLineRenderer()
    {
        lineRenderer.gameObject.SetActive(false);       
    }
    
    private void CreateLineRenderer()
    {
        lineRenderer.positionCount = ropeSegmentsList.Count; // Set the number of positions in the LineRenderer.
        lineRenderer.gameObject.SetActive(true);          // Activate the LineRenderer GameObject.
    }

    // Updates the positions of the LineRenderer to match the current positions of the rope segments.
    void UpdateLineRenderer()
    {
        // Exit if the rope is not IsAttached or if there are no segments.
        if (ropeSegmentsList == null)
        {
            return;
        }

        if (ropeSegmentsList.Count == 0)
        {
            return;
        }

        // Update each position in the LineRenderer to correspond to newVelocity segment's position.
        for (int i = 0; i < ropeSegmentsList.Count; i++)
        {
            //Debug.Log(i);
            var segment = ropeSegmentsList[i];

            if (segment == null)
            {
                return;
            }

            lineRenderer.SetPosition(i, segment.transform.position);
        }
    }
    #endregion
}
