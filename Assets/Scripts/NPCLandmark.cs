using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLandmark : MonoBehaviour
{

    public BuildingNode node;
    public string buildingName;
    public Vector3 location;

    private int level;
    private BoxCollider2D _trigger;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider2D>();
    }

    public void PopulateNodeParent()
    {

        node = GetComponentInParent<BuildingNode>();
        if (node == null)
        {
            Debug.LogError("ERROR: parent node is null");
        }
        level = node.level;
        buildingName = node.GetBuildingName();

        location = transform.position;//transform.TransformPoint(_trigger.bounds.center); //node.transform.parent.transform.TransformPoint(_trigger.bounds.center) + node.transform.parent.transform.position;
        //Debug.Log($"{ToString()}\n " +
        //    $"Landmark position: {transform.position}\n " +
        //    $"trigger center: {_trigger.bounds.center}\n " +
        //    $"transformed: {transform.TransformPoint(_trigger.bounds.center)}\n " +
        //    $"{node.transform.gameObject.name}: {node.transform.position}\n " +
        //    $"{node.transform.parent.gameObject.name}: {node.transform.parent.position}\n " +
        //    $"location: {location}\n");
    }

    public override string ToString()
    {
        return $"{gameObject.name} in {transform.parent.parent.parent.gameObject.name}";
    }

    public Vector3[] GetRoute(BuildingNode startNode)
    {
        Vector3[] routeFromOutside = GetRouteFromOutside();

        if (startNode == null) // source is outside
        {
            return routeFromOutside;
        }
        else
        {
            Vector3[] routeToOutside = startNode.GetRouteToOutside();
            //Debug.Log($"Route from {location.ToString()} to outside: {Helper.ArrayToString(routeToOutside)}");

            if (node == null) // this landmark (destination) is outside 
            {
                Vector3[] returnVector = new Vector3[routeToOutside.Length + 1];
                routeToOutside.CopyTo(returnVector, 0);
                new Vector3[] { location }.CopyTo(returnVector, routeToOutside.Length);

                return returnVector;
            }
            else if (node == startNode) // source is inside the same node
            {
                return new Vector3[] { location };
            }
            else // source is inside some other node
            {
                Vector3[] returnVector = new Vector3[routeToOutside.Length + routeFromOutside.Length + 1];
                routeToOutside.CopyTo(returnVector, 0);
                routeFromOutside.CopyTo(returnVector, routeToOutside.Length);
                new Vector3[] { location }.CopyTo(returnVector, routeToOutside.Length + routeFromOutside.Length);
                return returnVector;
            }
        }
        
    }

    public Vector3[] GetRouteFromOutside()
    {
        Vector3[] routeFromOutside = node.GetRouteFromOutside();
        Vector3[] returnVector = new Vector3[routeFromOutside.Length + 1];
        routeFromOutside.CopyTo(returnVector, 0);
        new Vector3[] {location}.CopyTo(returnVector, routeFromOutside.Length);

        return returnVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCMovement npc = collision.GetComponent<NPCMovement>();


        if (npc != null)
        {
            //Debug.Log($"Landmark {ToString()} colliders with {collision.name}. Same level? NPC: {npc.currentLevel}, Landmark: {level}");

            if (npc.currentLevel == level)
            {
                npc.RegisterCollision(this);

            }
        } 
    }
}
