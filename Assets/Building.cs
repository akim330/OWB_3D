using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] BuildingType type;
    public int id;

    protected NPCLandmark[] npcLandmarks;
    protected DoorMeshLink[] doorMeshLinks;

    public string GetBuildingName()
    {
        return gameObject.name;
    }

    public NPCLandmark GetRandomLandmark()
    {
        if (npcLandmarks.Length == 0)
        {
            return null;
        }
        else
        {
            return npcLandmarks[Random.Range(0, npcLandmarks.Length)];

        }
    }

    public void PopulateProperties()
    {

        npcLandmarks = GetComponentsInChildren<NPCLandmark>();
        doorMeshLinks = GetComponentsInChildren<DoorMeshLink>();

        foreach(DoorMeshLink link in doorMeshLinks)
        {

            link.SetPoints();
        }
    }
}
