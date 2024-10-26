using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomCharacterEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public string glitchCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public float initialGlitchDuration = 0.1f;
    public float totalGlitchDuration = 5f; // Total duration for the glitch effect
    public float slowDownFactor = 2f; // Factor by which the glitch duration will increase

    private string originalText;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        originalText = textComponent.text;
        StartCoroutine(ApplyRandomCharacterEffect());
    }

    public IEnumerator ApplyRandomCharacterEffect()
    {
        float elapsedTime = 0f;
        float currentGlitchDuration = initialGlitchDuration;

        while (elapsedTime < totalGlitchDuration)
        {
            string newText = "";

            // Generate a new text with random characters
            for (int i = 0; i < originalText.Length; i++)
            {
                char randomChar = glitchCharacters[Random.Range(0, glitchCharacters.Length)];
                newText += randomChar;
            }

            // Apply the new text
            textComponent.text = newText;

            // Wait for the current glitch duration
            yield return new WaitForSeconds(currentGlitchDuration);

            // Revert to the original text for a brief moment
            textComponent.text = originalText;
            yield return new WaitForSeconds(currentGlitchDuration);

            // Update elapsed time and increase the glitch duration
            elapsedTime += currentGlitchDuration * 2;
            currentGlitchDuration += slowDownFactor * currentGlitchDuration * Time.deltaTime;
        }

        // Ensure the original text is set at the end
        textComponent.text = originalText;
    }
}
