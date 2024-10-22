using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateOverObject : MonoBehaviour
{
    public GameObject levitatingObject; // The object that will levitate
    public GameObject targetObject; // The object over which the levitating object will hover

    public float levitationHeight = 2f; // The height at which the object will levitate
    public float levitationSpeed = 2f; // The speed of levitation
    public float oscillationAmplitude = 0.5f; // The amplitude of the oscillation
    public float oscillationFrequency = 1f; // The frequency of the oscillation

    private Vector3 initialPosition;

    void Start()
    {
        if (levitatingObject == null || targetObject == null)
        {
            Debug.LogError("LevitatingObject or TargetObject is not assigned.");
            return;
        }

        // Store the initial position of the levitating object
        initialPosition = levitatingObject.transform.position;
    }

    void Update()
    {
        if (levitatingObject != null && targetObject != null)
        {
            // Calculate the target position directly above the target object
            Vector3 targetPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + levitationHeight, targetObject.transform.position.z);

            // Move the levitating object towards the target position smoothly
            levitatingObject.transform.position = Vector3.Lerp(levitatingObject.transform.position, targetPosition, Time.deltaTime * levitationSpeed);

            // Apply oscillation effect to create a floating effect
            float oscillation = Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude;
            levitatingObject.transform.position += new Vector3(0, oscillation, 0);
        }
    }
}
