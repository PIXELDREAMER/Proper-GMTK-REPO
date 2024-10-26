using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    PlayerController player;

    private CanvasGroup canvasGroup;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient targetGradient;
    [SerializeField] private Image faceImage;
    [SerializeField] private List<Sprite> facesList = new();
    [SerializeField] private List<float> thresholds = new();

    [SerializeField] private float maxStaminaDelayTime = 0.2f; // Time to wait before fading out
    [SerializeField] private float fadeOutSpeed = 5f; // Speed of fading out
    [SerializeField] private float fadeInSpeed = 2.0f; // Speed of fading in

    private int currentFaceIndex = 0;
    private float maxStaminaTimer = 0f; // Timer to track how long stamina is at max
    private bool isFadingOut = false;

    private void Start()
    {
        player = PlayerController.Instance;
        player.OnCanMoveChanged += PlayerController_OnCanMoveChanged;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Start with the stamina bar not appearing

        UpdateBar();
    }

    private void PlayerController_OnCanMoveChanged(object sender, System.EventArgs e)
    {
        UpdateBar();
    }

    private void LateUpdate()
    {
        UpdateBar();
        HandleCanvasFade();
    }

    private void UpdateBar()
    {
        float targetFill = player.StaminaCounter / player.MaxStamina;
        targetFill = Mathf.Clamp01(targetFill);

        fillImage.fillAmount = targetFill;
        fillImage.color = targetGradient.Evaluate(targetFill);
        UpdateFace(targetFill);

        // Handle timer and fading out if stamina is at max
        if (Mathf.Approximately(targetFill, 1.0f))
        {
            maxStaminaTimer += Time.deltaTime;
            if (maxStaminaTimer >= maxStaminaDelayTime)
            {
                isFadingOut = true;
            }
        }
        else
        {
            maxStaminaTimer = 0f;
            isFadingOut = false;
            // Fade in quickly if stamina is not at max
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1f, fadeInSpeed * Time.deltaTime);
        }
    }

    private void UpdateFace(float stamina)
    {
        if (currentFaceIndex < thresholds.Count - 1 && stamina >= thresholds[currentFaceIndex + 1])
        {
            currentFaceIndex++;
            faceImage.sprite = facesList[currentFaceIndex];
        }
        else if (currentFaceIndex > 0 && stamina < thresholds[currentFaceIndex])
        {
            currentFaceIndex--;
            faceImage.sprite = facesList[currentFaceIndex];
        }

        faceImage.color = targetGradient.Evaluate(stamina);
    }

    private void HandleCanvasFade()
    {
        if (isFadingOut)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, fadeOutSpeed * Time.deltaTime);
        }
    }
}
