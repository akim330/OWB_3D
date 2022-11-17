using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MaterialContainer
{
    Image,
    SpriteRenderer
}

public class ColorChanger : MonoBehaviour
{
    protected Material colorMaterial;

    [SerializeField] protected Shader shader;

    protected PaletteManager palette;

    [SerializeField] protected MaterialContainer container = MaterialContainer.SpriteRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        colorMaterial = new Material(shader);

        // Assign this new material to every child's SpriteRenderer
        // When we make changes to the material by replacing a color, all the children will be affected

        if (container == MaterialContainer.SpriteRenderer)
        {
            SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.material = colorMaterial;
            }
        }
        else if (container == MaterialContainer.Image)
        {
            Image[] images = GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.material = colorMaterial;
            }
        }
        else
        {
            Debug.LogError("Not implemented");
        }


            palette = (PaletteManager)FindObjectOfType(typeof(PaletteManager));

        UpdateAllColors();

        //UpdateSkin();
        //UpdateClothes();

    }

    public virtual void UpdateAllColors()
    {
    }
}
