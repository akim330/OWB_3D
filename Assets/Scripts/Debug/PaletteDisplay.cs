using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteDisplay : MonoBehaviour
{
    //[SerializeField] string baseColorName;
    [SerializeField] PaletteManager clctr;
    //[SerializeField] string baseColorName;
    //[SerializeField] int nDarker;
    //[SerializeField] int nLighter;


    SpriteRenderer[] squareRenderers;

    // Start is called before the first frame update
    void Start()
    {

        squareRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void DisplayPalette(Color[] seq)
    {
        for (int i = 0; i < squareRenderers.Length; i++)
        {
            if (i < seq.Length)
            {
                squareRenderers[i].gameObject.SetActive(true);
                squareRenderers[i].color = seq[i];

            }
            else
            {
                squareRenderers[i].gameObject.SetActive(false);
            }
        }
        
    }

}
