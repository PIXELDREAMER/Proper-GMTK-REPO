using System.Collections;
using UnityEngine;
using TMPro;

public class GlitchEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public Color glitchColor = Color.red;
    public float glitchDuration = 0.1f;

    private TMP_TextInfo textInfo;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        textInfo = textComponent.textInfo;
    }

    public IEnumerator ApplyGlitchEffect()
    {
        while (true)
        {
            textComponent.ForceMeshUpdate();
            textInfo = textComponent.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32;

                Color32[] originalColors = new Color32[4];
                for (int j = 0; j < 4; j++)
                {
                    originalColors[j] = vertexColors[vertexIndex + j];
                }

                for (int j = 0; j < 4; j++)
                {
                    vertexColors[vertexIndex + j] = glitchColor;
                }

                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                yield return new WaitForSeconds(glitchDuration);

                for (int j = 0; j < 4; j++)
                {
                    vertexColors[vertexIndex + j] = originalColors[j];
                }
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
        }
    }
}
