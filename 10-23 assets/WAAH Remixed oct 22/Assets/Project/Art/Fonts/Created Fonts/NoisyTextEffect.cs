using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoisyTextEffect : MonoBehaviour
{
    public Image image;
    public GameObject TitleScreenImage;
    public float duration = 5f; // Duration for the transition
    public int noiseResolution = 256; // Resolution for the noise texture
    

    public Color color1 = Color.black;
    public Color color2 = Color.white;
    public Color color3 = Color.gray;

    private Texture2D texture;
    private float timer = 0f;
    private bool isFading = false;
    private Color[] colors;

    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
            TitleScreenImage = GetComponent<GameObject>();
        }

        if (image.sprite == null)
        {
            Debug.LogError("Image component does not have a sprite assigned.");
            return;
        }

        // Create a new Texture2D with higher resolution for finer grains
        texture = new Texture2D(noiseResolution, noiseResolution);
        colors = new Color[texture.width * texture.height];
        InitializeNoisyTexture();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        StartCoroutine(StaticEffectCoroutine());
        StartFading();
        
    }

    void Update()
    {
        if (isFading)
        {
            timer += Time.deltaTime;
            float alphaFactor = Mathf.Clamp01(timer / duration);
            UpdateNoisyTexture(alphaFactor);
        }
    }

    void InitializeNoisyTexture()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color randomColor = GetRandomColor();
                colors[y * texture.width + x] = randomColor;
                texture.SetPixel(x, y, randomColor);
            }
        }
        texture.Apply();
    }

    void UpdateNoisyTexture(float alphaFactor)
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                int index = y * texture.width + x;
                Color originalColor = GetRandomColor(); // Get a new random color continuously
                Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, alphaFactor));
                texture.SetPixel(x, y, fadedColor);
            }
        }
        texture.Apply();
    }

    Color GetRandomColor()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return color1;
            case 1:
                return color2;
            case 2:
                return color3;
            default:
                return color1;
        }
    }

    IEnumerator StaticEffectCoroutine()
    {
        while (!isFading)
        {
            InitializeNoisyTexture();
            yield return new WaitForSeconds(0.1f); // Adjust the frequency of noise updates
            
        }
        yield return new WaitForSeconds(duration);
        TitleScreenImage.SetActive(false);

        
    }

    public void StartFading()
    {
        isFading = true;

        


    }
}
