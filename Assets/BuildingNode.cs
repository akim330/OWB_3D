using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNode : MonoBehaviour
{
    public Door[] doors;
    public int level;
    public Sprite[] sprites;
    public Vector2[] positions;

    private Platform[] platforms;

    private BuildingTree _parentTree;

    public OutwardDoor[] outwardDoors;
    public InwardDoor[] inwardDoors;

    private void Awake()
    {
        _parentTree = GetComponentInParent<BuildingTree>();
    }

    public virtual void Deactivate()
    {
        //foreach (Door door in doors)
        //{
        //    door.gameObject.SetActive(false);
        //}

        //foreach (Platform platform in platforms)
        //{
        //    platform.gameObject.SetActive(false);
        //}
    }

    public virtual void Activate()
    {
        foreach (Door door in doors)
        {
            door.gameObject.SetActive(true);
        }

        foreach (Platform platform in platforms)
        {
            platform.gameObject.SetActive(true);
        }
    }
    public virtual void PopulateProperties()
    {
        // Get all sprites
        //SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();


        //for (int i = 0; i < renderers.Length; i++)
        //{
        //    sprites[i] = renderers[i].sprite;
        //}

        // Get all positions

        BuildingNodeBlock[] allNodeBlocks = GetComponentsInChildren<BuildingNodeBlock>();

        positions = new Vector2[allNodeBlocks.Length];
        sprites = new Sprite[allNodeBlocks.Length];

        var j = 0;
        //Debug.Log($"number of all transforms: {allTransforms.Length}");
        //Debug.Log($"Parent ID: {GetInstanceID()}");
        foreach (BuildingNodeBlock block in allNodeBlocks)
        {
            positions[j] = block.transform.position;
            sprites[j] = block.GetComponent<SpriteRenderer>().sprite;
            j++;
        }

        //Debug.Log($"DEBUG {ToString()}: allNodeBlocks.Length {allNodeBlocks.Length}, j: {j}");


        //Transform[] allTransforms = GetComponentsInChildren<Transform>();
        //Transform[] transforms = new Transform[allTransforms.Length - 1];
        //var j = 0;
        ////Debug.Log($"number of all transforms: {allTransforms.Length}");
        ////Debug.Log($"Parent ID: {GetInstanceID()}");
        //foreach (Transform childTransform in allTransforms)
        //{
        //    //Debug.Log($"Child ID: {childTransform.GetInstanceID()}");

        //    if (childTransform.GetInstanceID() != transform.GetInstanceID())
        //    {
        //        //Debug.Log($"Passed");

        //        transforms[j] = childTransform;
        //        j++;
        //    }
        //}

        //positions = new Vector2[transforms.Length];

        //for (int i = 0; i < transforms.Length; i++)
        //{
        //    positions[i] = transforms[i].position;
        //}

        // Get doors
        doors = GetComponentsInChildren<Door>(true);
        inwardDoors = GetComponentsInChildren<InwardDoor>(true);
        outwardDoors = GetComponentsInChildren<OutwardDoor>(true);

        // Get platforms
        platforms = GetComponentsInChildren<Platform>(true);

        // Set all child object layers to appropriate one
        if (level != 0)
        {
            int layer = LayerMask.NameToLayer($"Level{level}Colliders");

            //Debug.Log($"Setting layer to Level{level}Colliders =  {layer}");
            foreach (Transform childTransform in GetComponentsInChildren<Transform>(true))
            {
                childTransform.gameObject.layer = layer;
            }
        }
    }

    public BuildingNode(int level)
    {
        this.level = level;
    }

    public Vector2 GetGroundDoorLocation()
    {
        return _parentTree.GetGroundDoorLocation();
    }

    public Vector3[] GetRouteFromOutside()
    {
        return _parentTree.GetRouteToNode(this);
    }

    public Vector3[] GetRouteToNode(BuildingNode node)
    {
        Vector3[] resultFromChild;

        foreach (InwardDoor inwardDoor in inwardDoors)
        {
            if (inwardDoor.destinationNode == node)
            {
                return new Vector3[] { inwardDoor.transform.position, new Vector3(inwardDoor.transform.position.x, inwardDoor.transform.position.y, 10 * (inwardDoor.destinationNode.level + 1)) };
            }

            resultFromChild = inwardDoor.destinationNode.GetRouteToNode(node);

            if (resultFromChild != null) // Found a good path through this child
            {
                Vector3[] returnArray = new Vector3[2 + resultFromChild.Length];
                new Vector3[] { inwardDoor.transform.position, new Vector3(inwardDoor.transform.position.x, inwardDoor.transform.position.y, 10 * (inwardDoor.destinationNode.level + 1))}.CopyTo(returnArray, 0);
                resultFromChild.CopyTo(returnArray, 2);
                return returnArray;
            }
        }

        return null;
    }

    public Vector3[] GetRouteToOutside()
    {
        if (level == 0)
        {
            return new Vector3[] { };
        }

        OutwardDoor outwardDoor;

        outwardDoor = outwardDoors[0];
        
        Vector3[] resultFromParent = outwardDoor.destinationNode.GetRouteToOutside();
        Vector3[] returnArray = new Vector3[2 + resultFromParent.Length];
        new Vector3[] { outwardDoor.transform.position, new Vector3 ( outwardDoor.transform.position.x, outwardDoor.transform.position.y, 10 * (outwardDoor.destinationNode.level + 1) - 1)}.CopyTo(returnArray, 0);
        resultFromParent.CopyTo(returnArray, 2);

        return returnArray;
    }

    public string GetBuildingName()
    {
        return _parentTree.gameObject.name;
    }

    public override string ToString()
    {
        return $"{gameObject.name} in {_parentTree.name}";
    }
}
