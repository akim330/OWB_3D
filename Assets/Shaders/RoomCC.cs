using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCC : ColorChanger
{
    // (0) If necessary, override Start if you don't want to change the color for all child sprites
    protected override void Start()
    {
        container = MaterialContainer.SpriteRenderer;

        colorMaterial = new Material(shader);

        // Assign this new material to every child's SpriteRenderer
        // When we make changes to the material by replacing a color, all the children will be affected
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.material = colorMaterial;

        palette = (PaletteManager)FindObjectOfType(typeof(PaletteManager));

        UpdateAllColors();

    }

    // (1) Put all function calls from section (2) in this function
    public override void UpdateAllColors()
    {
        UpdateRandomBase();

        //UpdateRandomHair();
        //UpdateRandomSkin();
        //UpdateRandomShirt();
        //UpdateRandomPants();
    }

    // (2) Make all color-replacement functions
    public void UpdateRandomBase()
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
                UpdateRandomBase();
            }

            // Set colors
            else
            {
                //Debug.Log($"Setting base colors to {currentColors[0]}, {currentColors[1]}");
                colorMaterial.SetColor("_Base0", currentColors[0]);
                colorMaterial.SetColor("_Base1", currentColors[1]);
                colorMaterial.SetColor("_Base2", currentColors[2]);
            }
        }
    }
}
