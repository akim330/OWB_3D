using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonColorChanger : ColorChanger
{
    //private Material colorMaterial;

    //[SerializeField] Shader shader;

    //private PaletteManager palette;

    private Color[] currentHairColors;
    private Color[] currentSkinColors;
    private Color[] currentShirtColors;
    private Color[] currentPantsColors;

    //private SpriteRenderer[] renderers;

    protected override void Start()
    {
        container = MaterialContainer.SpriteRenderer;

        base.Start();
    }

    public override void UpdateAllColors()
    {
        UpdateRandomHair();
        UpdateRandomSkin();
        UpdateRandomShirt();
        UpdateRandomPants();
    }

    public void UpdateRandomHair()
    {
        currentHairColors = palette.GetRandomShades(1, 1);
        if (currentHairColors != null)
        {
            if (
                (currentHairColors[1] == palette.colors["brown"] && currentHairColors[2] == palette.colors["dark_orange"]) ||
                (currentHairColors[1] == palette.colors["blue_green3"] && currentHairColors[2] == palette.colors["green1"]) ||
                (currentHairColors[1] == palette.colors["maroon"] && currentHairColors[2] == palette.colors["dark_orange"])
            )
            {
                UpdateRandomHair();
            }
            else
            {
                colorMaterial.SetColor("_Hair0", currentHairColors[0]);
                colorMaterial.SetColor("_Hair1", currentHairColors[1]);
                colorMaterial.SetColor("_Hair2", currentHairColors[2]);
            }
        }
    }

    public void UpdateRandomSkin()
    {
        currentSkinColors = palette.GetRandomSkinPalette();

        if (currentSkinColors != null)
        {
            colorMaterial.SetColor("_Skin0", currentSkinColors[0]);
            colorMaterial.SetColor("_Skin1", currentSkinColors[1]);
            colorMaterial.SetColor("_Skin2", currentSkinColors[2]);
        }

    }

    public void UpdateRandomShirt()
    {
        currentShirtColors = palette.GetRandomShades(1, 0);

        if (currentShirtColors != null)
        {
            colorMaterial.SetColor("_Shirt0", currentShirtColors[0]);
            colorMaterial.SetColor("_Shirt1", currentShirtColors[1]);
        }
    }

    public void UpdateRandomPants()
    {
        currentPantsColors = palette.GetRandomShades(1, 0);

        if (currentPantsColors != null)
        {
            colorMaterial.SetColor("_Pants0", currentPantsColors[0]);
            colorMaterial.SetColor("_Pants1", currentPantsColors[1]);
        }
    }
}
