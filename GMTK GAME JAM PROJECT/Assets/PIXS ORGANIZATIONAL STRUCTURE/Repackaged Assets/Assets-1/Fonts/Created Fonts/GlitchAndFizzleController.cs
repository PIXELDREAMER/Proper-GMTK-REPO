using System.Collections;
using UnityEngine;
using TMPro;

public class GlitchAndFizzleController : MonoBehaviour
{
    public TMP_Text textComponent;
    public float totalGlitchDuration = 5f;
    public float slowdownDuration = 2f;
    public float initialInterval = 0.1f;

    private GlitchEffect glitchEffect;
    private RandomCharacterEffect randomCharacterEffect;
    private FizzleEffect fizzleEffect;

    void Start()
    {
        glitchEffect = GetComponent<GlitchEffect>();
        randomCharacterEffect = GetComponent<RandomCharacterEffect>();
        fizzleEffect = GetComponent<FizzleEffect>();

        StartCoroutine(GlitchAndFizzleRoutine());
    }

    IEnumerator GlitchAndFizzleRoutine()
    {
        float elapsedTime = 0f;

        // Start the effects
        StartCoroutine(glitchEffect.ApplyGlitchEffect());
       // StartCoroutine(randomCharacterEffect.ApplyRandomCharacterEffect());
        StartCoroutine(fizzleEffect.ApplyFizzleEffect());

        while (elapsedTime < totalGlitchDuration)
        {
            float interval = Mathf.Lerp(initialInterval, initialInterval * 5, (elapsedTime - (totalGlitchDuration - slowdownDuration)) / slowdownDuration);
            yield return new WaitForSeconds(interval);
            elapsedTime += interval;
        }

        // Stop the effects
        StopAllCoroutines();
    }
}
