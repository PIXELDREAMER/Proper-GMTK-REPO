using System;
using UnityEngine;

// The movement currently doesn't feels very good, I'd recommend you to change it
// Also fix the size changing as the mass and gravity change are behaving weirdly
[DefaultExecutionOrder(-5)]
public class BaloonController : MonoBehaviourSingleton<BaloonController>
{
    PlayerController playerController;
    public event EventHandler OnChangedSize;

    public float happyAmount = 1f;

    [Header("Movement")]
    [SerializeField] private float moveForce = 60f; // Force for the player moveInput
    [SerializeField] private float staminaMatterThreshold = 0.5f;
    [SerializeField] private float staminaFactor = 0.5f;
    [SerializeField] private float minStaminaMultiplier = 0.2f;

    [Header("Manual Changing Size")]
    [SerializeField] private Vector2 sizeLimits = new (1f, 2.5f); // 1f is the minimum or the ballon gets ridicuosly small
    [SerializeField] private float increaseSize = 0.25f;
    [SerializeField] private float decreaseSize = 0.5f;

    [Header("Size Changing Physics")]
    [SerializeField] private bool setPhysicsAtAwake = true; // sets mass at awake, what more can I say?
    [SerializeField] private float massScaleFactor = 1f; // Adjust this to control how mass changes with scale
    [SerializeField] private float gravityScaleFactor = 1f; // Adjust this to control how mass changes with scale
    public Rigidbody2D Rb { get; private set; } // Everyone can have the rigidbody, only the ballon can edit it
    private Vector2 moveInput;

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        
        if (setPhysicsAtAwake)
        {
            SetSizePhysics();
        }

        playerController = PlayerController.Instance;
    }

    void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }
        // Check for input to change hats. Z for previous hat, X for next hat.
        if (Input.GetKey(KeyCode.Z))
        {
            IncreaseSize(decreaseSize * Time.deltaTime, sizeLimits);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            IncreaseSize(increaseSize * Time.deltaTime, sizeLimits);
        }

        // Get input from the player, replace with the input system, here'matteredStamina a good video if you wanna do that:
        // https://youtu.be/Yjee_e4fICc?si=6PDmztkzKolTPhdc
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Left (-1) and Right (1)
        moveInput.y = Input.GetAxisRaw("Vertical");   // Down (-1) and Up (1)
    }

    void FixedUpdate()
    {
        if (moveInput == Vector2.zero)
        {
            return;
        }

        // Apply force to the Rigidbody2D
        float stamina = playerController.StaminaCounter;
        float targetStamina = stamina > staminaMatterThreshold ? 1f : stamina;
        float staminaMultiplier = Mathf.Max(targetStamina * staminaFactor, minStaminaMultiplier);

        Rb.AddForce(staminaMultiplier * moveForce * moveInput);
    }

    public void IncreaseSize(float sizeIncrease, Vector2 limits)
    {
        Vector3 scaleIncrease = new Vector3(sizeIncrease, sizeIncrease, 0);
        Vector3 targetScale = transform.localScale + scaleIncrease;

        targetScale.x = Mathf.Clamp(targetScale.x, limits.x, limits.y);
        targetScale.y = Mathf.Clamp(targetScale.y, limits.x, limits.y);

        if (targetScale == transform.localScale)
        {
            return;
        }

        //SetSizePhysics();
        happyAmount += sizeIncrease;
        Debug.Log($"Changed Size! Original {happyAmount}; Increased {happyAmount - sizeIncrease}");
        OnChangedSize?.Invoke(this, EventArgs.Empty);
    }

    private void SetSizePhysics()
    {
        //// Update the balloon'matteredStamina mass based on the new scale
        //float volume = Mathf.Pow(transform.localScale.x, 3);
        //Rb.mass += volume * massScaleFactor;
        //Rb.gravityScale += volume * gravityScaleFactor;
    }
}
