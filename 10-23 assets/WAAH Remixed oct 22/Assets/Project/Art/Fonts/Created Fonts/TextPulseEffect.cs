using System.Collections;
using UnityEngine;
using TMPro;

public class TextPulseEffect : MonoBehaviour
{
    public TMP_Text textComponent; // Reference to the TextMeshPro component
    public float pulseDuration = 1f; // Duration for one full pulse cycle (enlarge + shrink)
    public float maxScale = 1.5f; // Maximum scale factor
    public float minScale = 1f; // Minimum scale factor

    private Coroutine pulseCoroutine;

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }

        StartPulsing();
    }

    public void StartPulsing()
    {
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
        }
        pulseCoroutine = StartCoroutine(PulseEffectCoroutine());
    }

    public void StopPulsing()
    {
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            pulseCoroutine = null;
        }
    }

    private IEnumerator PulseEffectCoroutine()
    {
        while (true)
        {
            // Enlarge
            yield return ScaleText(minScale, maxScale, pulseDuration / 2);

            // Shrink
            yield return ScaleText(maxScale, minScale, pulseDuration / 2);
        }
    }

    private IEnumerator ScaleText(float startScale, float endScale, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(startScale, endScale, elapsedTime / duration);
            textComponent.transform.localScale = new Vector3(scale, scale, scale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.transform.localScale = new Vector3(endScale, endScale, endScale);
    }
}
