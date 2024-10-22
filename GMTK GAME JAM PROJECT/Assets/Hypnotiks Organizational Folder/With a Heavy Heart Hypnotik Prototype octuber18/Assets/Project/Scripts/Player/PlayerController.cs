using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-4)]
public class PlayerController : MonoBehaviourSingleton<PlayerController>
{
    public event EventHandler OnCanMoveChanged;

    #region GENERAL
    Rope rope;

    [HideInInspector] public Vector2 lastRespawnPosition; // Make it public so other scripts can edit it (no need to add a separate method)

    [field: SerializeField] public string PlayerTag { get; private set; } = "Player";
    #endregion

    #region UNTIED TIMER
    [field: SerializeField] public float MaxStamina { get; private set; } = 5f;
    [SerializeField] private float tiedStaminaSpeed = 1.5f;
    [SerializeField] private float staminaRecoverSpeed = 0.5f;
    public float StaminaCounter { get; private set; }
    public bool CanMove { get; private set; } = true;
    #endregion

    #region JUMPING
    [Header("Jumping")]
    public float groundCheckDistance = 0.2f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float timeToJump = 10f;

    [SerializeField] private Rigidbody2D targetRb;
    private float timeOnGround;
    #endregion

    private void Awake()
    {
        rope = Rope.Instance;
        StaminaCounter = MaxStamina;
    }

    private void Update()
    {
        // Please separate the script for the text, this is just for testing

        #region TIE COUNTER
        CanMove = StaminaCounter > 0f;

        if (StaminaCounter > 0 && !rope.IsAttached)
        {
            if (!CanMove)
            {
                return;
            }

            StaminaCounter -= Time.deltaTime;
            Debug.Log("Decreasing");

            if (StaminaCounter <= 0)
            {
                Debug.Log("Ended");
                OnCanMoveChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else if (StaminaCounter < MaxStamina && rope.IsAttached)
        {
            float targetSpeed = CanMove ? tiedStaminaSpeed : staminaRecoverSpeed;
            StaminaCounter += Time.deltaTime * targetSpeed;
        }
        #endregion

        if (!CanMove)
        {
            return;
        }

        #region GROUND DETECTION
        bool isGrounded = Physics2D.Raycast(targetRb.transform.position, -targetRb.transform.up, groundCheckDistance, groundLayer);

        if (isGrounded && !rope.IsAttached)
        {
            timeOnGround += Time.deltaTime;

            if (timeOnGround >= timeToJump)
            {
                Jump();
                timeOnGround = 0f; // Reset timer after jumping
            }
            else if (StaminaCounter <= 1f)
            {
                Jump();
                timeOnGround = 0f; // Reset timer after jumping
            }
        }
        else
        {
            timeOnGround = 0f; // Reset timer if not on the ground
        }
        #endregion
    }

    void Jump()
    {
        Debug.Log("Player jumped");
        targetRb.AddForce(targetRb.transform.up * jumpForce, ForceMode2D.Force);
    }

    public void AddStamina(float amount)
    {
        StaminaCounter = Mathf.Clamp(StaminaCounter + amount, 0f, MaxStamina);
        CanMove = StaminaCounter > 0f;

        if (StaminaCounter <= 0)
        {
            Debug.Log("Ended");
            OnCanMoveChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Lose()
    {
        // Your lose logic here (communicate to a game manager)
        Debug.Log("You lose");
    }

    public void Respawn()
    {
        // Your player respawn logic here
    }
}