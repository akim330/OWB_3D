using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCC : ColorChanger
{

    [SerializeField] string colorName;
    // Start is called before the first frame update
    public override void UpdateAllColors()
    {
        colorMaterial.SetColor("_Base0", palette.colors[colorName]);

    }
}
