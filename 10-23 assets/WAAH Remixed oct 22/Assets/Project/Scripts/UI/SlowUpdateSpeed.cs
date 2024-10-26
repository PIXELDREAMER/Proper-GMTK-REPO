using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowUpdateSpeed : MonoBehaviour
{
    public Transform targetTransform; // Reference to the player's transform
    public float positionSmoothSpeed = 5f; // Adjust for how smooth the UI follows the player

    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset between the player and the UI element
        offset = transform.position - targetTransform.position;
    }

    void LateUpdate()
    {
        // Smoothly update the position
        Vector3 targetPosition = targetTransform.position + offset;

        // Maintain the current rotation (or align with camera, if preferred)
        Vector3 position = Vector3.Lerp(transform.position, targetPosition, positionSmoothSpeed * Time.deltaTime);
        transform.SetPositionAndRotation(position, Quaternion.identity);
    }
}
