using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class BuildingData
//{
//    public BuildingTree tree;
//    public int id;
//    public BuildingType type;
//    public Vector2 location;
//    public Vector2 doorLocation;
//    public int nFloors;

//    public BuildingData(BuildingTree _tree, int _id, BuildingType _type, Vector2 _location, Vector2 _doorLocation, int _nFloors)
//    {
//        tree = _tree;
//        id = _id;
//        location = _location;
//        doorLocation = _doorLocation;
//        type = _type;
//        nFloors = _nFloors;
//    }
//}

public class TownManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] GameObject streetLampPrefab;

    private int _townStartX;
    private int _townEndX;

    //private int nPeople;
    private int nBuildings;

    private List<NPCRole> npcRoles;
    private Queue<NPCRole> npcRolesQueue;


    private List<Building> buildingsList;
    private NPCRole[] rolesToAdd;

    [SerializeField] GameObject townParent;

    [SerializeField] TerrainGeneration terrainGeneration;

    public void Startup()
    {
        nBuildings = 0;
        //nPeople = 0;

        buildingsList = new List<Building>();

        rolesToAdd = new NPCRole[]
        {
            NPCRole.Lumberjack, NPCRole.Farmer, NPCRole.Blacksmith, NPCRole.Grocer
        };

        terrainGeneration.Startup();
    }

    public void GenerateTown(out GameObject parent, out Vector3Int size)
    {
        parent = Instantiate(townParent, Vector3.zero, Quaternion.identity);
        parent.name = $"Town";

        int sizeX = 64;
        int sizeZ = 32;
        int spacing = 5;

        GameObject currentInstantiatedObject;
        Vector3Int currentSize;
        int currentX = 0;
        int maxZ = 0;
        int maxY = 0;

        void FormatTransform(GameObject obj, GameObject parent)
        {
            obj.transform.parent = parent.transform;
            obj.transform.localPosition = new Vector3(currentX, 0, 0);
            currentX += currentSize.x + spacing;

            if (currentSize.y > maxY)
            {
                maxY = currentSize.y;
            }
            if (currentSize.z > maxZ)
            {
                maxZ = currentSize.z;
            }
        }

        // Place mart
        Managers.Building.GetMart(out currentInstantiatedObject, out currentSize);
        RegisterGameObjectAsBuilding(currentInstantiatedObject);
        FormatTransform(currentInstantiatedObject, parent);

        // Place cafe
        Managers.Building.GetCafe(out currentInstantiatedObject, out currentSize);
        RegisterGameObjectAsBuilding(currentInstantiatedObject);
        FormatTransform(currentInstantiatedObject, parent);

        for (int i = 0; i < 2; i++)
        {
            // Place apartment
            Managers.Building.GetApartment(1, 3, out currentInstantiatedObject, out currentSize);
            RegisterGameObjectAsBuilding(currentInstantiatedObject);
            FormatTransform(currentInstantiatedObject, parent);
        }

        size = new Vector3Int(currentX, maxY, maxZ);

        // Place lamps

        for (int x = -12; x < currentX; x += 12)
        {
            currentInstantiatedObject = Instantiate(streetLampPrefab, parent.transform, false);
            currentInstantiatedObject.transform.position = new Vector3(x, 0, -12);
        }

        PopulateBuildingProperties();


    }

    private void PopulateBuildingProperties()
    {
        foreach (Building building in buildingsList)
        {
            // Debug.Log($"populating {building.gameObject.name}");
            building.PopulateProperties();
        }
    }

    private void RegisterGameObjectAsBuilding(GameObject obj)
    {
        Building building = obj.GetComponent<Building>();
        if (building != null)
        {
            buildingsList.Add(building);
        }
        else
        {
            Debug.LogError($"This gameobject {obj.name} doesn't have a Building component!");
        }
    }

    public void SetTownCoordinates(int startX, int endX)
    {
        _townStartX = startX;
        _townEndX = endX;
    }

    private void RandomizeNPCRoles()
    {
        int nRoles = buildingsList.Count;

        npcRoles = new List<NPCRole>();

        for (int i = 0; i < nRoles - rolesToAdd.Length; i++)
        {
            npcRoles.Add(NPCRole.None);
        }

        for (int i = 0; i < rolesToAdd.Length; i++)
        {
            npcRoles.Add(rolesToAdd[i]);
        }

        // Randomize npcRoles
        for (int i = 0; i < npcRoles.Count; i++)
        {
            NPCRole temp = npcRoles[i];
            int randomIndex = Random.Range(i, npcRoles.Count);
            npcRoles[i] = npcRoles[randomIndex];
            npcRoles[randomIndex] = temp;
        }
        npcRolesQueue = new Queue<NPCRole>(npcRoles);
    }

    public void PlaceNPCsInHouses()
    {
        if (npcRolesQueue == null)
        {
            RandomizeNPCRoles();
        }

        foreach(Building building in buildingsList)
        {
            Managers.NPC.PlaceNPCInBuilding(npcRolesQueue.Dequeue(), building);
        }
    }

    //public void LogHabitableBlocks(BuildingTree tree)
    //{
    //    foreach (BuildingNodeBlock block in tree.GetComponentsInChildren<BuildingNodeBlock>())
    //    {
    //        if (block.habitable)
    //        {
    //            habitableBuildings.Add(block);
    //        }
    //    }
    //}

    //public void PlacePeopleOutside()
    //{
    //    int coolDown = 0;

    //    for (int x = _townStartX; x < _townEndX; x++)
    //    {
    //        // Check if cool down is 0 to ensure minimum spacing
    //        if (coolDown == 0)
    //        {
    //            // Place person with probability 1/5 only if the ground is level
    //            if (Random.Range(0, 5) == 0)
    //            {
    //                GameObject npcObj;

    //                Managers.NPC.GetNPC(out npcObj);

    //                coolDown = 0;
    //            }
    //        }
    //        else if (coolDown > 0)
    //        {
    //            coolDown--;
    //        }
    //        else
    //        {
    //            Debug.LogError($"ERROR: coolDown is negative: {coolDown}");
    //        }
    //    }
    //}

    //public void LogBuilding(BuildingTree building)
    //{
    //    buildingTrees.Add(building);

    //    // TODO: Add building data
    //    buildingData.Add(new BuildingData(
    //        _tree: building,
    //        _id: nBuildings,
    //        _type: building.type,
    //        _location: building.transform.position,
    //        _doorLocation: building.GetGroundDoorLocation(),
    //        _nFloors: building.nFloors
    //    ));

    //    nBuildings++;
    //}

    //public Vector2 GetRandomBuildingLocation()
    //{
    //    return buildingData[Random.Range(0, buildingData.Count)].doorLocation;
    //}

    public NPCLandmark GetRandomLandmark()
    {
        NPCLandmark chosen = null;
        while (chosen == null)
        {
            chosen = buildingsList[Random.Range(0, buildingsList.Count)].GetRandomLandmark();

        }
        return chosen;
    }



    //public BuildingNode GetRandomNode()
    //{
    //    return buildingTrees[Random.Range(0, buildingTrees.Count)].GetRandomNode();
    //}
}
