using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkColorChanger : MonoBehaviour
{
    private Material colorMaterial;
    [SerializeField] string baseSkinColor;
    [SerializeField] string baseClothesColor;
    [SerializeField] PaletteDisplay skinDisplay;
    [SerializeField] PaletteDisplay clothesDisplay;


    [SerializeField] Shader shader;

    private PaletteManager palette;
    [System.NonSerialized] public Color[] currentSkinColors;
    [System.NonSerialized] public Color[] currentClothesColors;

    private SpriteRenderer[] childRenderers;
    private SpriteRenderer parentRenderer;

    // Start is called before the first frame update
    void Start()
    {
        colorMaterial = new Material(shader);

        childRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in childRenderers)
        {
            renderer.material = colorMaterial;
        }

        parentRenderer = GetComponent<SpriteRenderer>();
        parentRenderer.enabled = false;

        palette = (PaletteManager)FindObjectOfType(typeof(PaletteManager));

        //UpdateSkin();
        //UpdateClothes();
        UpdateRandomSkin();
        UpdateRandomClothes();
    }

    public void UpdateAll()
    {
        UpdateSkin();
        UpdateClothes();
    }

    // Update is called once per frame
    public void UpdateSkin()
    {
        currentSkinColors = palette.GetShades(baseSkinColor, 3, 1);

        // 3, 2, 1, 0, 1
        // 0, 1, 2, 3, 4

        if (currentSkinColors != null)
        {
            colorMaterial.SetColor("_Color0", currentSkinColors[0]);
            colorMaterial.SetColor("_Color1", currentSkinColors[3]);
            colorMaterial.SetColor("_Color2", currentSkinColors[4]);
        }

        if (skinDisplay != null)
        {
            skinDisplay.DisplayPalette(currentSkinColors);
        }
    }

    public void UpdateRandomSkin()
    {
        Color[] currentSkinColors = palette.GetRandomSkinPalette();

        if (currentSkinColors != null)
        {
            colorMaterial.SetColor("_Color0", currentSkinColors[0]);
            colorMaterial.SetColor("_Color1", currentSkinColors[1]);
            colorMaterial.SetColor("_Color2", currentSkinColors[2]);
        }

        if (skinDisplay != null)
        {
            skinDisplay.DisplayPalette(currentSkinColors);
        }
    }

    // Update is called once per frame
    public void UpdateClothes()
    {
        currentClothesColors = palette.GetShades(baseClothesColor, 1, 0);

        if (currentClothesColors != null)
        {
            colorMaterial.SetColor("_Color3", currentClothesColors[0]);
            colorMaterial.SetColor("_Color4", currentClothesColors[1]);
        }

        if (clothesDisplay != null)
        {
            clothesDisplay.DisplayPalette(currentClothesColors);
        }
    }

    public void UpdateRandomClothes()
    {
        currentClothesColors = palette.GetRandomShades(1, 0);

        if (currentClothesColors != null)
        {
            colorMaterial.SetColor("_Color3", currentClothesColors[0]);
            colorMaterial.SetColor("_Color4", currentClothesColors[1]);
        }

        if (clothesDisplay != null)
        {
            clothesDisplay.DisplayPalette(currentClothesColors);
        }
    }
}
