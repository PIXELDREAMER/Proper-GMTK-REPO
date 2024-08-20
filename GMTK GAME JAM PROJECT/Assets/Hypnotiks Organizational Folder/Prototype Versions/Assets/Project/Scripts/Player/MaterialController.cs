using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMaterial
{
    [Range(0f, 1f)] public float saturation;
    public float temperature;
    public float tint;
    public float contrast;
    public float brigthness;
}

public class MaterialController : MonoBehaviour
{
    [Header("This script changes the materials of all objects that use this material")]
    //private PlayerController player;
    private BaloonController baloon;

    [SerializeField] private Material material;

    [SerializeField] private PlayerMaterial sadMaterialSettings;
    [SerializeField] private PlayerMaterial happyMaterialSettings;
    private readonly PlayerMaterial normalMaterialSettings;

    private const string SATURATION = "_Saturation";

    private void Awake()
    {
        baloon = BaloonController.Instance;
        baloon.OnChangedSize += Baloon_OnChangedSize;

        normalMaterialSettings.saturation = material.GetFloat(SATURATION);
    }

    private void Baloon_OnChangedSize(object sender, System.EventArgs e)
    {
        UpdateColors();
    }

    private void UpdateColors()
    {
        float happyAmount = baloon.happyAmount;



        PlayerMaterial targetSettings = happyAmount >= 1f ? happyMaterialSettings: sadMaterialSettings;
        float saturationAmount = Mathf.Lerp(normalMaterialSettings.saturation, targetSettings.saturation, happyAmount);

        material.SetFloat("Saturation", saturationAmount);
    }
}
