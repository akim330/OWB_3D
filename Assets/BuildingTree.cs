using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTree : MonoBehaviour
{
    public BuildingType type;
    public int nFloors;

    public List<BuildingNode> nodes;
    private NPCLandmark[] _landmarks;

    private BuildingNode _activeNode;
    private int _activeLevel;

    private BuildingNode _exteriorNode;



    //[SerializeField] private GameObject stairBase;
    //[SerializeField] private GameObject stairLevel;

    private void Awake()
    {
        _activeLevel = 0;
        nodes = new List<BuildingNode>();
    }

    public void TransitionToNode(BuildingNode node)
    {
        // Deactivate current level
        _activeNode.Deactivate();

        // Set new level
        _activeLevel = node.level;
        _activeNode = node;
        _activeNode.Activate();
    }

    public Sprite[] GetActiveSprites()
    {
        return _activeNode.sprites;
    }

    public Vector2[] GetActivePositions()
    {
        return _activeNode.positions;
    }

    public void AddNode(BuildingNode node, bool exterior)
    {
        nodes.Add(node);

        if (exterior)
        {
            if (_exteriorNode != null)
            {
                Debug.LogError("Already assigned an exterior node");
            }
            _exteriorNode = node;
        }
    }

    public void PopulateAllProperties()
    {
        _landmarks = GetComponentsInChildren<NPCLandmark>(true);
        foreach (NPCLandmark landmark in _landmarks)
        {
            landmark.PopulateNodeParent();
        }

        foreach (BuildingNode node in nodes)
        {
            //Debug.Log($"Populating properties for {node.gameObject.name}");
            node.PopulateProperties();
        }
    }

    public Vector2 GetGroundDoorLocation()
    {
        return _exteriorNode.doors[0].transform.position;
    }

    public NPCLandmark GetRandomLandmark()
    {
        return _landmarks[Random.Range(0, _landmarks.Length)];
    }

    public Vector3[] GetRouteToNode(BuildingNode node)
    {
        return _exteriorNode.GetRouteToNode(node);
    }

    //public BuildingNode GetRandomNode()
    //{
    //    return _nodes[Random.Range(0, _nodes.Count)];
    //}



    //public void PopulateDoorParents()
    //{
    //    doors = GetComponentsInChildren<BuildingDoor>();
    //    //Debug.Log($"Number of doors: {doors.Length}");

    //    foreach (BuildingDoor door in doors)
    //    {
    //        door.buildingParent = this;
    //    }
    //}

    //public Vector2 GetGroundDoorLocation()
    //{
    //    return _exteriorParent.GetGroundDoorLocation();
    //}
}
