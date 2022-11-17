using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofaCC : ColorChanger
{
    protected override void Start()
    {
        container = MaterialContainer.SpriteRenderer;

        base.Start();

    }

    // (1) Put all function calls from section (2) in this function
    public override void UpdateAllColors()
    {
        UpdateRandomBase();
        UpdateRandomCushion();

        //UpdateRandomHair();
        //UpdateRandomSkin();
        //UpdateRandomShirt();
        //UpdateRandomPants();
    }

    // (2) Make all color-replacement functions
    public void UpdateRandomBase()
    {
        Color[] currentColors;
        currentColors = palette.GetRandomShades(1, 0);
        if (currentColors != null)
        {
            // Check for bad color combos (have to manually input)
            if (
                (currentColors[0] == palette.colors["brown"] && currentColors[1] == palette.colors["dark_orange"]) ||
                (currentColors[0] == palette.colors["blue_green3"] && currentColors[1] == palette.colors["green1"]) ||
                (currentColors[0] == palette.colors["maroon"] && currentColors[1] == palette.colors["dark_orange"])
            )
            {
                // Call this function
                UpdateRandomBase();
            }

            // Set colors
            else
            {
                colorMaterial.SetColor("_Base0", currentColors[0]);
                colorMaterial.SetColor("_Base1", currentColors[1]);
            }
        }
    }

    public void UpdateRandomCushion()
    {

        Color[] currentColors;
        currentColors = palette.GetRandomShades(1, 1);
        if (currentColors != null)
        {
            // Check for bad color combos (have to manually input)
            if (
                (currentColors[0] == palette.colors["brown"] && currentColors[1] == palette.colors["dark_orange"]) ||
                (currentColors[0] == palette.colors["blue_green3"] && currentColors[1] == palette.colors["green1"]) ||
                (currentColors[0] == palette.colors["maroon"] && currentColors[1] == palette.colors["dark_orange"])
            )
            {
                // Call this function
                UpdateRandomCushion();
            }

            // Set colors
            else
            {
                //Debug.Log($"Setting cushion colors to {currentColors[0]}, {currentColors[1]}, {currentColors[2]}");

                colorMaterial.SetColor("_Cushion0", currentColors[0]);
                colorMaterial.SetColor("_Cushion1", currentColors[1]);
                colorMaterial.SetColor("_Cushion2", currentColors[2]);
            }
        }
    }

}
