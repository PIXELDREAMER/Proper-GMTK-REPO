using System.Collections;
using UnityEngine;
using TMPro;

public class GlitchTextEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public float glitchDuration = 0.1f; // Duration of each glitch effect
    public float glitchInterval = 0.5f; // Time between glitches
    public Vector2 positionRange = new Vector2(1, 1); // Range for position offset
    public Color glitchColor = Color.red; // Color used during glitch
    public bool randomCharacterReplacement = true; // Whether to replace characters with random ones
    public string glitchCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Characters to use for glitch replacement
    public float fizzleAmount = 0.5f; // Amount of fizzle effect on pixels
    public float totalGlitchDuration = 5f; // Total duration for the glitch effect
    public float slowdownDuration = 2f; // Duration of the slowdown period

    private string originalText;
    private TMP_TextInfo textInfo;
    private bool isGlitching = true;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        originalText = textComponent.text;
        textInfo = textComponent.textInfo;

        StartCoroutine(GlitchRoutine());
    }

    IEnumerator GlitchRoutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < totalGlitchDuration)
        {
            float interval = Mathf.Lerp(glitchInterval, glitchInterval * 5, (elapsedTime - (totalGlitchDuration - slowdownDuration)) / slowdownDuration);
            yield return new WaitForSeconds(interval);

            StartCoroutine(GlitchEffect());

            elapsedTime += interval;
        }

        isGlitching = false;
        RestoreOriginalText();
    }

    IEnumerator GlitchEffect()
    {
        textComponent.ForceMeshUpdate();
        textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            // Save original vertex colors and positions
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32;
            Vector3[] vertices = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;

            Color32[] originalColors = new Color32[4];
            Vector3[] originalVertices = new Vector3[4];
            for (int j = 0; j < 4; j++)
            {
                originalColors[j] = vertexColors[vertexIndex + j];
                originalVertices[j] = vertices[vertexIndex + j];
            }

            // Apply glitch effect
            for (int j = 0; j < 4; j++)
            {
                vertexColors[vertexIndex + j] = glitchColor;
                vertices[vertexIndex + j] += new Vector3(
                    Random.Range(-positionRange.x, positionRange.x),
                    Random.Range(-positionRange.y, positionRange.y),
                    0);
            }

            // Apply fizzle effect
            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] += new Vector3(
                    Random.Range(-fizzleAmount, fizzleAmount),
                    Random.Range(-fizzleAmount, fizzleAmount),
                    0);
            }

            // Optional: Replace character with a random one
            if (randomCharacterReplacement)
            {
                char randomChar = glitchCharacters[Random.Range(0, glitchCharacters.Length)];
                textComponent.text = textComponent.text.Substring(0, i) + randomChar + textComponent.text.Substring(i + 1);
                textComponent.ForceMeshUpdate();
                textInfo = textComponent.textInfo;
            }

            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32 | TMP_VertexDataUpdateFlags.Vertices);

            yield return new WaitForSeconds(glitchDuration);

            // Restore original vertex colors and positions
            for (int j = 0; j < 4; j++)
            {
                vertexColors[vertexIndex + j] = originalColors[j];
                vertices[vertexIndex + j] = originalVertices[j];
            }
            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32 | TMP_VertexDataUpdateFlags.Vertices);
        }
    }

    void RestoreOriginalText()
    {
        textComponent.text = originalText;
        textComponent.ForceMeshUpdate();
    }
}
