using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData
{
    public BuildingTree tree;
    public int id;
    public BuildingType type;
    public Vector2 location;
    public Vector2 doorLocation;
    public int nFloors;

    public BuildingData(BuildingTree _tree, int _id, BuildingType _type, Vector2 _location, Vector2 _doorLocation, int _nFloors)
    {
        tree = _tree;
        id = _id;
        location = _location;
        doorLocation = _doorLocation;
        type = _type;
        nFloors = _nFloors;
    }
}

public class TownManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    //[SerializeField] GameObject[] peoplePrefabs;
    //[SerializeField] GameObject basePersonPrefab;
    //[SerializeField] AnimatorOverrideController[] npcOverrides;

    private int _townStartX;
    private int _townEndX;

    //private int nPeople;

    private List<BuildingData> buildingData;
    private List<BuildingTree> buildingTrees;

    private int nBuildings;

    private List<NPCRole> npcRoles;
    private Queue<NPCRole> npcRolesQueue;

    private List<BuildingNodeBlock> habitableBuildings;
    private NPCRole[] rolesToAdd;

    public void Startup()
    {
        nBuildings = 0;
        //nPeople = 0;

        habitableBuildings = new List<BuildingNodeBlock>();

        buildingData = new List<BuildingData>();
        buildingTrees = new List<BuildingTree>();

        rolesToAdd = new NPCRole[]
        {
            NPCRole.Lumberjack, NPCRole.Farmer, NPCRole.Blacksmith, NPCRole.Grocer
        };
    }

    public void SetTownCoordinates(int startX, int endX)
    {
        _townStartX = startX;
        _townEndX = endX;
    }

    private void RandomizeNPCRoles()
    {
        int nRoles = habitableBuildings.Count;

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

        foreach(BuildingNodeBlock building in habitableBuildings)
        {
            Managers.NPC.PlaceNPCInNodeBlock(npcRolesQueue.Dequeue(), building);
        }
    }

    public void LogHabitableBlocks(BuildingTree tree)
    {
        foreach (BuildingNodeBlock block in tree.GetComponentsInChildren<BuildingNodeBlock>())
        {
            if (block.habitable)
            {
                habitableBuildings.Add(block);
            }
        }
    }

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

    public void LogBuilding(BuildingTree building)
    {
        buildingTrees.Add(building);

        // TODO: Add building data
        buildingData.Add(new BuildingData(
            _tree: building,
            _id: nBuildings,
            _type: building.type,
            _location: building.transform.position,
            _doorLocation: building.GetGroundDoorLocation(),
            _nFloors: building.nFloors
        ));

        nBuildings++;
    }

    public Vector2 GetRandomBuildingLocation()
    {
        return buildingData[Random.Range(0, buildingData.Count)].doorLocation;
    }

    public NPCLandmark GetRandomLandmark()
    {
        return buildingTrees[Random.Range(0, buildingTrees.Count)].GetRandomLandmark();
    }



    //public BuildingNode GetRandomNode()
    //{
    //    return buildingTrees[Random.Range(0, buildingTrees.Count)].GetRandomNode();
    //}
}
