using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviourSingleton<BaloonController>
{
    public event EventHandler OnChangedSize;

    public float moveForce = 60f; // Speed of the player movement
    [SerializeField] private float massScaleFactor = 1f; // Adjust this to control how mass changes with scale
    [SerializeField] private Vector2 sizeLimits = new Vector2(0.5f, 1.5f);
    public Rigidbody2D rb { get; private set; }
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        IncreaseSize(0f);
    }

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal"); // Left (-1) and Right (1)
        movement.y = Input.GetAxisRaw("Vertical");   // Down (-1) and Up (1)
    }

    void FixedUpdate()
    {
        // Apply force to the Rigidbody2D
        rb.AddForce(movement * moveForce);
    }

    public void IncreaseSize(float sizeIncrease)
    {
        Vector3 scaleChange = new Vector3(sizeIncrease, sizeIncrease, 0);
        Vector3 targetScale = transform.localScale += scaleChange;

        targetScale.x = Mathf.Clamp(targetScale.x, sizeLimits.x, sizeLimits.y);
        targetScale.y = Mathf.Clamp(targetScale.x, sizeLimits.x, sizeLimits.y);

        if (targetScale == transform.localScale)
        {
            return;
        }

        // Update the balloon's mass based on the new scale
        float volume = Mathf.Pow(transform.localScale.x, 3);
        rb.mass = volume * massScaleFactor;

        OnChangedSize?.Invoke(this, EventArgs.Empty);
    }
}
