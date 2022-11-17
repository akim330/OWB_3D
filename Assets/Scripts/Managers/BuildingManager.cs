using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BuildingType
{
    Store,
    Cafe,
    Mart,
    Residence
}

public class BuildingManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [Header("Commercial")]
    [SerializeField] private BuildingPiece store_ext;
    [SerializeField] private BuildingPiece store_int;
    [SerializeField] private BuildingPiece cafe_ext;
    [SerializeField] private BuildingPiece cafe_int;
    [SerializeField] private BuildingPiece mart_ext;

    [Header("Apartment")]
    [SerializeField] private BuildingPiece apartment_ext_base_0;
    [SerializeField] private BuildingPiece apartment_ext_base_1;
    [SerializeField] private BuildingPiece apartment_ext_level_0;
    [SerializeField] private BuildingPiece apartment_ext_level_1;

    [SerializeField] private BuildingPiece apartment_int_base_0;
    [SerializeField] private BuildingPiece apartment_int_level_0;

    [SerializeField] private BuildingPiece staircase_base;
    [SerializeField] private BuildingPiece staircase_level;

    [Header("BuildingTree")]
    [SerializeField] private GameObject buildingTreePrefab;
    [SerializeField] private GameObject buildingNodePrefab;
    [SerializeField] private GameObject interiorBuildingNodePrefab;

    //[SerializeField] private GameObject exteriorParentPrefab;
    //[SerializeField] private GameObject buildingParentPrefab;
    //[SerializeField] private GameObject interiorParentPrefab;

    private int currentID;

    [SerializeField] private int incZ;

    //private int interiorZ = 50;

    public void Startup()
    {
        currentID = 0;
    }

    //private BuildingPiece TypeToBuildingTile(BuildingType type)
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

    public void GetCommercialTile(BuildingType type, out GameObject parent, out int maxWidth)
    {
        // Instantiate BuildingParent prefab
        parent = Instantiate(buildingTreePrefab, Vector3.zero, Quaternion.identity);
        parent.name = $"Building {currentID} ({type})";
        currentID++;
        BuildingTree buildingTree = parent.GetComponent<BuildingTree>();

        // Set BuildingParent attributes
        buildingTree.type = BuildingType.Residence;
        buildingTree.nFloors = 1;

        BuildingPiece exteriorPiece;
        BuildingPiece interiorPiece;

        if (type == BuildingType.Cafe)
        {
            exteriorPiece = cafe_ext;
            interiorPiece = cafe_int;
        }
        else if (type == BuildingType.Store)
        {
            exteriorPiece = store_ext;
            interiorPiece = store_int;
        }
        else if (type == BuildingType.Mart)
        {
            exteriorPiece = mart_ext;
            interiorPiece = store_int;
        }
        else
        {
            Debug.LogError($"Unrecognized BuildingType: {type}");
            exteriorPiece = null;
            interiorPiece = null;
        }
        
        // Set maxwidth
        maxWidth = exteriorPiece.width;

        // Instantiate BuildingNodes
        GameObject exteriorNodeObj = Instantiate(buildingNodePrefab, parent.transform, false);
        exteriorNodeObj.name = "Exterior Node";
        exteriorNodeObj.transform.localPosition = new Vector3(0, 0, 0);
        BuildingNode exteriorNode = exteriorNodeObj.GetComponent<BuildingNode>();
        exteriorNode.level = 0;

        buildingTree.AddNode(exteriorNode, exterior: true);

        GameObject interiorNodeObj = Instantiate(interiorBuildingNodePrefab, parent.transform, false);
        interiorNodeObj.name = "Interior Node";
        interiorNodeObj.transform.localPosition = new Vector3(0, 0, incZ);
        BuildingNode interiorNode = interiorNodeObj.GetComponent<InteriorBuildingNode>();
        interiorNode.level = 1;

        buildingTree.AddNode(interiorNode, exterior: false);

        // Instantiate BuildingPieces under BuildingNodes
        GameObject exteriorChild = Instantiate(exteriorPiece.gameObject, exteriorNodeObj.transform, false);
        exteriorChild.transform.localPosition = new Vector3(0, 0, 0);

        //middleChild = Instantiate(stairPrefab, _interiorParents[0].transform, false);
        //middleChild.transform.localPosition = new Vector3(0, 0, 0);

        GameObject interiorChild = Instantiate(interiorPiece.gameObject, interiorNodeObj.transform, false);
        interiorChild.transform.localPosition = new Vector3(0, 0, 0);

        InteriorColliderParent currentInteriorColliderParent;

        currentInteriorColliderParent = interiorChild.GetComponentInChildren<InteriorColliderParent>(true);
        if (currentInteriorColliderParent == null)
        {
            Debug.LogError($"Couldn't find interiorColliderParent in {interiorChild.name}");
        }

        currentInteriorColliderParent.CullColliders(BuildingStackPosition.Sole);
        
        // Tell doors which nodes to go to
        InwardDoor[] exteriorDoors = exteriorNode.GetComponentsInChildren<InwardDoor>(true);
        if (exteriorDoors.Length == 0)
        {
            Debug.Log("WARNING: No inward doors found!");
        }
        foreach (InwardDoor door in exteriorDoors)
        {
            //Debug.Log($"{parent.gameObject.name}, {exteriorNodeObj.name}, {door.gameObject.name}");
            door.destinationNode = interiorNode;
        }

        OutwardDoor[] interiorDoors = interiorNode.GetComponentsInChildren<OutwardDoor>(true);
        foreach (OutwardDoor door in interiorDoors)
        {
            door.destinationNode = exteriorNode;
        }

        buildingTree.PopulateAllProperties();

        //buildingParent.PopulateDoorParents();
        //interiorParent.PopulateProperties();

    }

    public void GetRandomApartment(int minLevel, int maxLevel, out GameObject parent, out int maxWidth)
    {
        //    // Instantiate BuildingParent prefab
        //    parent = Instantiate(buildingTreePrefab, Vector3.zero, Quaternion.identity);
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
        // Instantiate BuildingParent prefab
        parent = Instantiate(buildingTreePrefab, Vector3.zero, Quaternion.identity);
        parent.name = $"Building {currentID} (Apartment)";
        currentID++;
        BuildingTree buildingTree = parent.GetComponent<BuildingTree>();

        // Set BuildingParent attributes
        buildingTree.type = BuildingType.Residence;
        buildingTree.nFloors = Random.Range(minLevel, maxLevel + 1);

        BuildingPiece exteriorBasePiece;
        BuildingPiece interiorBasePiece;
        BuildingPiece exteriorLevelPiece;
        BuildingPiece interiorLevelPiece;

        BuildingPiece staircaseBasePiece;
        BuildingPiece staircaseLevelPiece;

        int style = Random.Range(0, 2);

        if (style == 0)
        {
            exteriorBasePiece = apartment_ext_base_0;
            exteriorLevelPiece = apartment_ext_level_0;

            interiorBasePiece = apartment_int_base_0;
            interiorLevelPiece = apartment_int_level_0;

            staircaseBasePiece = staircase_base;
            staircaseLevelPiece = staircase_level;

        }
        else if (style == 1)
        {
            exteriorBasePiece = apartment_ext_base_1;
            exteriorLevelPiece = apartment_ext_level_1;

            interiorBasePiece = apartment_int_base_0;
            interiorLevelPiece = apartment_int_level_0;

            staircaseBasePiece = staircase_base;
            staircaseLevelPiece = staircase_level;
        }
        else
        {
            Debug.LogError($"Style {style} not coded yet");
            exteriorBasePiece = null;
            exteriorLevelPiece = null;

            interiorBasePiece = null;
            interiorLevelPiece = null;

            staircaseBasePiece = null;
            staircaseLevelPiece = null;
        }

        // Set maxwidth
        maxWidth = exteriorBasePiece.width;

        // Variable placeholders to use while building nodes
        int currentHeight;
        BuildingPiece currentPiece;
        GameObject currentInstantiatedObject;
        InteriorColliderParent currentInteriorColliderParent;

        // (1) Make exterior node
        GameObject exteriorNodeObj = Instantiate(buildingNodePrefab, parent.transform, false);
        exteriorNodeObj.name = "Exterior Node";

        exteriorNodeObj.transform.localPosition = new Vector3(0, 0, 0);
        BuildingNode exteriorNode = exteriorNodeObj.GetComponent<BuildingNode>();
        exteriorNode.level = 0;
        buildingTree.AddNode(exteriorNode, exterior: true);

        // Build exteriorNode
        currentHeight = 0;

        for (var k = 0; k < buildingTree.nFloors; k++)
        {
            if (k == 0)
            {
                currentPiece = exteriorBasePiece;
            }
            else
            {
                currentPiece = exteriorLevelPiece;
            }

            currentInstantiatedObject = Instantiate(currentPiece.gameObject, exteriorNode.transform, false);
            currentInstantiatedObject.transform.localPosition = new Vector3(0, currentHeight, 0);

            currentHeight += currentPiece.height;
        }

        // (2) Make middle node
        GameObject middleNodeObj = Instantiate(interiorBuildingNodePrefab, parent.transform, false);
        middleNodeObj.name = "Middle Node";

        middleNodeObj.transform.localPosition = new Vector3(0, 0, incZ);
        BuildingNode middleNode = middleNodeObj.GetComponent<InteriorBuildingNode>();
        middleNode.level = 1;
        buildingTree.AddNode(middleNode, exterior: false);

        // Building middleNode
        currentHeight = 0;

        for (var k = 0; k < buildingTree.nFloors; k++)
        {
            if (k == 0)
            {
                currentPiece = staircaseBasePiece;
            }
            else
            {
                currentPiece = staircaseLevelPiece;
            }

            currentInstantiatedObject = Instantiate(currentPiece.gameObject, middleNode.transform, false);
            currentInstantiatedObject.transform.localPosition = new Vector3(0, currentHeight, 0);

            currentInteriorColliderParent = currentInstantiatedObject.GetComponentInChildren<InteriorColliderParent>(true);
            if (currentInteriorColliderParent == null)
            {
                Debug.LogError($"Couldn't find interiorColliderParent in {currentInstantiatedObject.name}");
            }

            if (buildingTree.nFloors == 1)
            {
                currentInteriorColliderParent.CullColliders(BuildingStackPosition.Sole);
            }
            else if (k == 0)
            {
                currentInteriorColliderParent.CullColliders(BuildingStackPosition.Bottom);
            }
            else if (k == buildingTree.nFloors - 1)
            {
                currentInteriorColliderParent.CullColliders(BuildingStackPosition.Top);
            }
            else
            {
                currentInteriorColliderParent.CullColliders(BuildingStackPosition.Middle);
            }

            currentHeight += currentPiece.height;
        }

        // (3) Make interior nodes

        GameObject[] interiorNodeObjs = new GameObject[buildingTree.nFloors];

        BuildingNode[] interiorNodes = new BuildingNode[buildingTree.nFloors];

        currentHeight = 0;

        for (var k = 0; k < buildingTree.nFloors; k++)
        {
            interiorNodeObjs[k] = Instantiate(interiorBuildingNodePrefab, parent.transform, false);
            interiorNodeObjs[k].name = $"Interior Node {k}";

            interiorNodeObjs[k].transform.localPosition = new Vector3(0, 0, 2 * incZ);
            interiorNodes[k] = interiorNodeObjs[k].GetComponent<InteriorBuildingNode>();
            interiorNodes[k].level = 2;
            buildingTree.AddNode(interiorNodes[k], exterior: false);

            if (k == 0)
            {
                currentPiece = interiorBasePiece;
            }
            else
            {
                currentPiece = interiorLevelPiece;
            }

            currentInstantiatedObject = Instantiate(currentPiece.gameObject, interiorNodeObjs[k].transform, false);
            currentInstantiatedObject.transform.localPosition = new Vector3(0, currentHeight, 0);

            currentInteriorColliderParent = currentInstantiatedObject.GetComponentInChildren<InteriorColliderParent>(true);
            if (currentInteriorColliderParent == null)
            {
                Debug.LogError($"Couldn't find interiorColliderParent in {currentInstantiatedObject.name}");
            }

            //if (buildingTree.nFloors == 1)
            //{
            currentInteriorColliderParent.CullColliders(BuildingStackPosition.Sole);
            //}
            //else if (k == 0)
            //{
            //    currentInteriorColliderParent.CullColliders(BuildingStackPosition.Bottom);
            //}
            //else if (k == buildingTree.nFloors - 1)
            //{
            //    currentInteriorColliderParent.CullColliders(BuildingStackPosition.Top);
            //}
            //else
            //{
            //    currentInteriorColliderParent.CullColliders(BuildingStackPosition.Middle);
            //}

            currentHeight += currentPiece.height;
        }

        // (4) Tell doors which nodes to go to
        InwardDoor[] exteriorDoors = exteriorNode.GetComponentsInChildren<InwardDoor>(true);
        foreach (Door door in exteriorDoors)
        {
            door.destinationNode = middleNode;
        }

        OutwardDoor[] middleDoorsOut = middleNode.GetComponentsInChildren<OutwardDoor>(true);
        foreach (OutwardDoor door in middleDoorsOut)
        {
            door.destinationNode = exteriorNode;
        }

        InwardDoor[] middleDoorsIn = middleNode.GetComponentsInChildren<InwardDoor>(true);
        int j = 0;
        foreach (InwardDoor door in middleDoorsIn)
        {
            door.destinationNode = interiorNodes[j];
            j++;
        }

        foreach (BuildingNode interiorNode in interiorNodes)
        {
            OutwardDoor[] interiorDoors = interiorNode.GetComponentsInChildren<OutwardDoor>(true);
            int k = 0;
            foreach (OutwardDoor door in interiorDoors)
            {
                door.destinationNode = middleNode;
                k++;
            }


        }



        buildingTree.PopulateAllProperties();

        // Log habitable building blocks with TownManager
        Managers.Town.LogHabitableBlocks(buildingTree);
    }

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
