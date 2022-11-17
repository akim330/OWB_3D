using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//public enum BuildingType
//{
//    Store,
//    Cafe,
//    Mart,
//    Residence
//}

public class x_BuildingManager : MonoBehaviour //, IGameManager
{
    //public ManagerStatus status { get; private set; }

    //[SerializeField] private BuildingTile storeTile;
    //[SerializeField] private BuildingTile cafeTile;
    //[SerializeField] private BuildingTile martTile;
    //[SerializeField] private BuildingTile building_base0;
    //[SerializeField] private BuildingTile building_base1;
    //[SerializeField] private BuildingTile building_level0;
    //[SerializeField] private BuildingTile building_level1;
    //[SerializeField] private GameObject stairPrefab;

    ////[SerializeField] private GameObject exteriorParentPrefab;
    //[SerializeField] private GameObject buildingParentPrefab;
    //[SerializeField] private GameObject interiorParentPrefab;

    //private int currentID;

    ////private int interiorZ = 50;

    //public void Startup()
    //{
    //    currentID = 0;
    //}

    //private BuildingTile TypeToBuildingTile(BuildingType type)
    //{
    //    if (type == BuildingType.Cafe)
    //    {
    //        return cafeTile;
    //    }
    //    else if (type == BuildingType.Store)
    //    {
    //        return storeTile;
    //    }
    //    else if (type == BuildingType.Mart)
    //    {
    //        return martTile;
    //    }
    //    else
    //    {
    //        Debug.LogError($"Unrecognized BuildingType: {type}");
    //        return null;
    //    }
    //}

    //public void GetCommercialTile(BuildingType type, out GameObject parent, out int maxWidth)
    //{
    //    // Instantiate BuildingParent prefab
    //    parent = Instantiate(buildingParentPrefab, Vector3.zero, Quaternion.identity);
    //    parent.name = $"Building {currentID} ({type})";
    //    currentID++;
    //    BuildingParent buildingParent = parent.GetComponent<BuildingParent>();

    //    // Set BuildingParent attributes
    //    buildingParent.type = BuildingType.Residence;
    //    buildingParent.nFloors = 1;


    //    // Create building
    //    BuildingTile currentTile = TypeToBuildingTile(type);
    //    buildingParent.CreateBuilding(currentTile);

    //    // Set maxwidth
    //    maxWidth = currentTile.width;

    //    //buildingParent.PopulateDoorParents();
    //    //interiorParent.PopulateProperties();

    //}

    //public void GetRandomBuilding(int minLevel, int maxLevel, out GameObject parent, out int maxWidth)
    //{
    //    // Instantiate BuildingParent prefab
    //    parent = Instantiate(buildingParentPrefab, Vector3.zero, Quaternion.identity);
    //    parent.name = $"Building {currentID} (Apartment)";
    //    currentID++;
    //    BuildingParent buildingParent = parent.GetComponent<BuildingParent>();

    //    // Set BuildingParent attributes
    //    buildingParent.type = BuildingType.Residence;
    //    buildingParent.nFloors = Random.Range(minLevel, maxLevel + 1);

    //    // Create building
    //    BuildingTile baseTile = GetRandomBase();
    //    BuildingTile levelTile = GetRandomLevel();
    //    buildingParent.CreateApartmentBuilding(baseTile, levelTile);

    //    maxWidth = Mathf.Max(baseTile.width, levelTile.width);

    //    //buildingParent.PopulateDoorParents();
    //    //interiorParent.PopulateProperties();

    //}

    //public BuildingTile GetRandomBase()
    //{
    //    int randomChoice = Random.Range(0, 2);

    //    if (randomChoice == 0)
    //    {
    //        return building_base0;
    //    }
    //    else if (randomChoice == 1)
    //    {
    //        return building_base1;
    //    }
    //    else
    //    {
    //        Debug.LogError("ERROR: randomChoice beyond currently coded options");
    //        return null;
    //    }
    //}

    //public BuildingTile GetRandomLevel()
    //{
    //    int randomChoice = Random.Range(0, 2);

    //    if (randomChoice == 0)
    //    {
    //        return building_level0;
    //    }
    //    else if (randomChoice == 1)
    //    {
    //        return building_level1;
    //    }
    //    else
    //    {
    //        Debug.LogError("ERROR: randomChoice beyond currently coded options");
    //        return null;
    //    }
    //}

    //public BuildingTile GetBuildingBaseTile()
    //{
    //    int randomChoice = Random.Range(0, 2);

    //    if (randomChoice == 0)
    //    {
    //        return building_base0;
    //    }
    //    else if (randomChoice == 1)
    //    {
    //        return building_base1;
    //    }
    //    else
    //    {
    //        Debug.LogError("ERROR: randomChoice beyond currently coded options");
    //        return null;
    //    }
    //}

    //public BuildingTile GetBuildingLevelTile()
    //{
    //    int randomChoice = Random.Range(0, 2);

    //    if (randomChoice == 0)
    //    {
    //        return building_level0;
    //    }
    //    else if (randomChoice == 1)
    //    {
    //        return building_level1;
    //    }
    //    else
    //    {
    //        Debug.LogError("ERROR: randomChoice beyond currently coded options");
    //        return null;
    //    }
    //}
}
