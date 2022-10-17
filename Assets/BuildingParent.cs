using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingParent : MonoBehaviour
{
    public void PopulateDoorParents()
    {
        BuildingDoor[] doors = GetComponentsInChildren<BuildingDoor>();
        //Debug.Log($"Number of doors: {doors.Length}");

        foreach (BuildingDoor door in doors)
        {
            door.buildingParent = this;
        }
    }
}
