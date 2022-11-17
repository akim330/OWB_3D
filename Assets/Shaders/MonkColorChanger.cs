using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkColorChanger : ColorChanger
{
    [SerializeField] string baseSkinColor;
    [SerializeField] string baseClothesColor;
    //[SerializeField] PaletteDisplay skinDisplay;
    //[SerializeField] PaletteDisplay clothesDisplay;


    [System.NonSerialized] public Color[] currentSkinColors;
    [System.NonSerialized] public Color[] currentClothesColors;

    // Start is called before the first frame update
    protected override void Start()
    {
        container = MaterialContainer.SpriteRenderer;

        base.Start();

        SpriteRenderer parentRenderer = GetComponent<SpriteRenderer>();
        parentRenderer.enabled = false;

    }

    public override void UpdateAllColors()
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

        //if (skinDisplay != null)
        //{
        //    skinDisplay.DisplayPalette(currentSkinColors);
        //}
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

        //if (skinDisplay != null)
        //{
        //    skinDisplay.DisplayPalette(currentSkinColors);
        //}
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

        //if (clothesDisplay != null)
        //{
        //    clothesDisplay.DisplayPalette(currentClothesColors);
        //}
    }

    public void UpdateRandomClothes()
    {
        currentClothesColors = palette.GetRandomShades(1, 0);

        if (currentClothesColors != null)
        {
            colorMaterial.SetColor("_Color3", currentClothesColors[0]);
            colorMaterial.SetColor("_Color4", currentClothesColors[1]);
        }

        //if (clothesDisplay != null)
        //{
        //    clothesDisplay.DisplayPalette(currentClothesColors);
        //}
    }
}
