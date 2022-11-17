using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNodeBlock : MonoBehaviour
{
    public bool habitable;

    public int level;
    public BuildingNode parentNode;

    private void Awake()
    {
        BuildingNode node = GetComponentInParent<BuildingNode>();

        if (node == null)
        {
            Debug.LogError("ERROR: No parent node found!");
        }
        else
        {
            parentNode = node;
            level = node.level;
        }
    }
}
