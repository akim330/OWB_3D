using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultilevelBuilding : Building
{
    private BuildingLevel[] levels;

    // Start is called before the first frame update
    void Start()
    {
        levels = GetComponentsInChildren<BuildingLevel>();

        if (levels.Length != 1)
        {
            for (int i = 0; i < levels.Length; i++)
            {

                if (i != 0)
                {
                    levels[i].DissolveFloor();
                }

                if (i != levels.Length - 1)
                {
                    levels[i].DissolveRoof();
                }
            }
        }

        npcLandmarks = GetComponentsInChildren<NPCLandmark>();
        doorMeshLinks = GetComponentsInChildren<DoorMeshLink>();

        if (gameObject.name == "Building 0 (Apartment)")
        {
            Debug.Log($"How many landmarks found? {npcLandmarks.Length}.");
        }

    }
}
