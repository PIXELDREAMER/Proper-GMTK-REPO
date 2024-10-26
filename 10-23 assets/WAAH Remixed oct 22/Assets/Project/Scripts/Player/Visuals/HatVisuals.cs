using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HatVisuals : MonoBehaviour
{
    // Reference to the SpriteRenderer component to change the player's hat sprite.
    private SpriteRenderer spriteRenderer;

    [Header("Changing Hats")]
    [SerializeField] private List<Sprite> hatsSprites = new(); // List of hat sprites to cycle through.
    private static int currentIndex; // Index to keep track of the current hat.

    [SerializeField] private float canChangeHatTime = 0.2f; // Cooldown time between hat changes.
    private bool isChangingHat; // Flag to check if the hat is currently changing.

    [SerializeField] private float hatInputBufferTime = 0.2f; // Buffer time to accept input for changing hats.
    private float hatInputCounter; // Counter to manage the input buffer timing.
    private int lastHatInput; // Stores the last input for hat change (-1 or 1).

    [SerializeField] private UnityEvent OnChangedHat; // Event triggered when the hat is changed.

    private const string HatPrefs = "Current Hat";
    private void Awake()
    {
        // Get the SpriteRenderer component IsAttached to the player.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize the first hat by forcing a change without invoking the event.
        currentIndex = PlayerPrefs.GetInt(HatPrefs, 0);
        spriteRenderer.sprite = hatsSprites[currentIndex];
    }

    private void Update()
    {
        // Check for input to change hats. Q for previous hat, E for next hat.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Q");
            hatInputCounter = hatInputBufferTime; // Reset the input buffer counter.
            lastHatInput = -1; // Set to decrease the hat index.
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("E");
            hatInputCounter = hatInputBufferTime;
            lastHatInput = 1; // Set to increase the hat index.
        }

        // If the input buffer is active and not currently changing hats, change the hat.
        if (hatInputCounter > 0f && !isChangingHat)
        {
            //Debug.Log("Performing! lastHatInput " + lastHatInput);
            ChangeHat(lastHatInput); // Change the hat and trigger the event.
            hatInputCounter = 0f; // Reset the counter.
            lastHatInput = 0;
        }
    }

    public void SetHat(int index)
    {
        // Set the current hat sprite.
        spriteRenderer.sprite = hatsSprites[index];
        PlayerPrefs.SetInt(HatPrefs, currentIndex);

        OnChangedHat?.Invoke();
    }

    // Method to change the hat based on input. Positive increase for next hat, negative for previous.
    public void ChangeHat(int increase)
    {
        //Debug.Log("Entered");

        // Loop through the hats based on the input amount. (Always do a positive amount of times)
        for (int i = 0; i < Mathf.Abs(increase); i++)
        {
            //Debug.Log("int i");

            // Could also use (int)Mathf.Sign(increase) to increase it, but this way is more readable and takes in account if the value is 0.
            currentIndex += increase >= 0 ? 1 : -1; // Adjust the index based on the input direction.
            //Debug.Log("Before: " + currentIndex);

            // Wrap around if the index exceeds the list bounds.
            if (currentIndex > hatsSprites.Count - 1)
            {
                currentIndex = 0;
            }
            else if (currentIndex < 0)
            {
                currentIndex = hatsSprites.Count - 1;
            }

            //Debug.Log("After: " + currentIndex);
        }

        // Set the current hat sprite.
        spriteRenderer.sprite = hatsSprites[currentIndex];
        PlayerPrefs.SetInt(HatPrefs, currentIndex);

        OnChangedHat?.Invoke();

        // Start the coroutine to handle the cooldown between hat changes.
        StartCoroutine(ChangingHatCorountine());
    }

    // Coroutine to manage the cooldown time after changing hats.
    public IEnumerator ChangingHatCorountine()
    {
        isChangingHat = true; // Set flag to true, indicating a hat change is in progress.
        yield return new WaitForSeconds(canChangeHatTime); // Wait for the cooldown time.
        isChangingHat = false; // Reset the flag after the cooldown.
    }
}
