using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;  // The rope segment prefab
    public int segmentCount = 10;         // Number of segments in the rope
    public LineRenderer lineRenderer;     // LineRenderer to visualize the rope

    private GameObject[] ropeSegments;    // Array to hold all rope segments

    [Header("Points")]
    public BaloonController baloon;          // Start point of the rope (first character)
    public Rigidbody2D player;            // End point of the rope (second character)

    public Transform startPoint;          // Start point of the rope (first character)
    public Transform endPoint;            // End point of the rope (second character)

    private bool attached = true;

    private void Start()
    {
        GenerateRope();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attached = !attached;

            if (attached)
            {
                GenerateRope();
            }
            else
            {
                DestroyRope();

                lineRenderer.gameObject.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        UpdateLineRenderer();
    }

    void GenerateRope()
    {
        ropeSegments = new GameObject[segmentCount];
        Rigidbody2D previousRB = baloon.rb;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector2 segmentPosition = Vector2.Lerp(startPoint.position, endPoint.position, (float)i / (segmentCount - 1));
            GameObject newSegment = Instantiate(ropeSegmentPrefab, segmentPosition, Quaternion.identity);
            Rigidbody2D currentRB = newSegment.GetComponent<Rigidbody2D>();

            HingeJoint2D hingeJoint = newSegment.GetComponent<HingeJoint2D>();
            hingeJoint.connectedBody = previousRB;

            ropeSegments[i] = newSegment;
            previousRB = currentRB;
        }

        // Connect the last segment to the end point
        HingeJoint2D endHinge = ropeSegments[segmentCount - 1].AddComponent<HingeJoint2D>();
        endHinge.connectedBody = player.GetComponent<Rigidbody2D>();

        CreateLineRenderer();
        UpdateLineRenderer();
    }

    void DestroyRope()
    {
        if (ropeSegments != null)
        {
            foreach (var ropeSegment in ropeSegments)
            {
                if (ropeSegment != null)
                {
                    ropeSegment.gameObject.SetActive(false);
                    Destroy(ropeSegment.gameObject, 0.2f);
                }
            }
        }
    }


    private void CreateLineRenderer()
    {
        lineRenderer.positionCount = ropeSegments.Length;
        lineRenderer.gameObject.SetActive(true);
    }

    void UpdateLineRenderer()
    {
        if (!attached || ropeSegments.Length == 0)
        {
            return;
        }

        for (int i = 0; i < ropeSegments.Length; i++)
        {
            lineRenderer.SetPosition(i, ropeSegments[i].transform.position);
        }
    }

}
