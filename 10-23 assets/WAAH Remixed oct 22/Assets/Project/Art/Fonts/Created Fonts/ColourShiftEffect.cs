using UnityEngine;
using TMPro;

public class ColourShiftEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float duration = 5f; // Duration to shift from startColor to endColor
    public bool loop = true; // Whether the color shift should loop back and forth

    private float timer = 0f;
    private bool reversing = false; // For looping back and forth

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }
        
        textComponent.color = startColor;
    }

    void Update()
    {
        if (loop)
        {
            // Loop back and forth
            timer += (reversing ? -1 : 1) * Time.deltaTime / duration;
            if (timer > 1f)
            {
                timer = 1f;
                reversing = true;
            }
            else if (timer < 0f)
            {
                timer = 0f;
                reversing = false;
            }
        }
        else
        {
            // Single transition from startColor to endColor
            timer += Time.deltaTime / duration;
            if (timer > 1f)
            {
                timer = 1f;
            }
        }

        textComponent.color = Color.Lerp(startColor, endColor, timer);
    }
}
