using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BuildingManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private BuildingTile store;
    [SerializeField] private BuildingTile building_base0;
    [SerializeField] private BuildingTile building_base1;
    [SerializeField] private BuildingTile building_level0;
    [SerializeField] private BuildingTile building_level1;

    //[SerializeField] private GameObject exteriorParentPrefab;
    [SerializeField] private GameObject buildingParentPrefab;
    [SerializeField] private GameObject interiorParentPrefab;

    private int interiorZ = 50;

    public void Startup()
    {

    }

    public BuildingTile GetStoreTile()
    {
        return store;
    }

    public void GetRandomBuilding(int minLevel, int maxLevel, out GameObject parent, out int maxWidth)
    {
        // Instantiate BuildingParent prefab
        parent = Instantiate(buildingParentPrefab, Vector3.zero, Quaternion.identity);
        BuildingParent buildingParent = parent.GetComponent<BuildingParent>();

        ExteriorParent exteriorParent = parent.GetComponentInChildren<ExteriorParent>();
        InteriorParent interiorParent = parent.GetComponentInChildren<InteriorParent>();
        //GameObject exteriorParent = new GameObject("Exterior");
        //exteriorParent.transform.parent = parent.transform;

        //GameObject interiorParent = Instantiate(interiorParentPrefab, parent.transform, false);
        //interiorParent.transform.localPosition = new Vector3(interiorParent.transform.position.x, interiorParent.transform.position.y, interiorZ);

        int nLevels = Random.Range(minLevel, maxLevel + 1);

        GameObject exteriorChild;
        GameObject interiorChild;

        int currentHeight = 0;
        maxWidth = 0;

        for (var i = 0; i < nLevels; i++)
        {
            BuildingTile currentTile;

            if (i == 0)
            {
                currentTile = GetRandomBase();
            }
            else
            {
                currentTile = GetRandomLevel();
            }
            
            exteriorChild = Instantiate(currentTile.exteriorTileObject, exteriorParent.transform, false);
            exteriorChild.transform.localPosition = new Vector3(0, currentHeight, 0);

            interiorChild = Instantiate(currentTile.interiorTileObject, interiorParent.transform, false);
            interiorChild.transform.localPosition = new Vector3(0, currentHeight, 0);

            currentHeight += currentTile.height;
            if (currentTile.width > maxWidth) maxWidth = currentTile.width;

        }

        buildingParent.PopulateDoorParents();
        interiorParent.PopulateProperties();

    }

    public BuildingTile GetRandomBase()
    {
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            return building_base0;
        }
        else if (randomChoice == 1)
        {
            return building_base1;
        }
        else
        {
            Debug.LogError("ERROR: randomChoice beyond currently coded options");
            return null;
        }
    }

    public BuildingTile GetRandomLevel()
    {
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            return building_level0;
        }
        else if (randomChoice == 1)
        {
            return building_level1;
        }
        else
        {
            Debug.LogError("ERROR: randomChoice beyond currently coded options");
            return null;
        }
    }

    public BuildingTile GetBuildingBaseTile()
    {
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            return building_base0;
        }
        else if (randomChoice == 1)
        {
            return building_base1;
        }
        else
        {
            Debug.LogError("ERROR: randomChoice beyond currently coded options");
            return null;
        }
    }

    public BuildingTile GetBuildingLevelTile()
    {
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            return building_level0;
        }
        else if (randomChoice == 1)
        {
            return building_level1;
        }
        else
        {
            Debug.LogError("ERROR: randomChoice beyond currently coded options");
            return null;
        }
    }
}
