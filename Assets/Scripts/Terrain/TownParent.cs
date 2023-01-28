using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownParent : MonoBehaviour
{
    private Building[] buildings;

    public void LogBuildings()
    {
        buildings = GetComponentsInChildren<Building>();

        foreach (Building building in buildings)
        {
            Managers.Town.RegisterGameObjectAsBuilding(building.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
