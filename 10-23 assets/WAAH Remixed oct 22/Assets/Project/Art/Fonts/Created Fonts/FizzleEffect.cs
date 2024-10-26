using System.Collections;
using UnityEngine;
using TMPro;

public class FizzleEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public Vector2 positionRange = new Vector2(1, 1);
    public float fizzleDuration = 0.1f;

    private TMP_TextInfo textInfo;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        textInfo = textComponent.textInfo;
    }

    public IEnumerator ApplyFizzleEffect()
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
                Vector3[] vertices = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;

                Vector3[] originalVertices = new Vector3[4];
                for (int j = 0; j < 4; j++)
                {
                    originalVertices[j] = vertices[vertexIndex + j];
                }

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] += new Vector3(
                        Random.Range(-positionRange.x, positionRange.x),
                        Random.Range(-positionRange.y, positionRange.y),
                        0);
                }

                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

                yield return new WaitForSeconds(fizzleDuration);

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] = originalVertices[j];
                }
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
            }
        }
    }
}
