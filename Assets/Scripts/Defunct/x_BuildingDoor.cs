using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x_BuildingDoor : MonoBehaviour
{
    private void Awake()
    {
        buildingTree = transform.parent.parent.parent.GetComponent<BuildingTree>();
        if (buildingTree == null)
        {
            Debug.LogError("Couldn't find BuildingParent!");
        }
    }

    public BuildingTree buildingTree;

    public int toLevel;

}
