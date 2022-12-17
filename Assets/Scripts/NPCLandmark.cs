using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLandmark : MonoBehaviour
{

    public Building building;
    public string buildingName;
    public Vector3 location;

    private BoxCollider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider>();

        PopulateFields();
    }

    public void PopulateFields()
    {

        building = GetComponentInParent<Building>();
        if (building == null)
        {
            building = transform.parent.GetComponentInParent<Building>();

            if (building == null)
            {
                Debug.LogError("ERROR: parent building is null");

            }
        }
        buildingName = building.GetBuildingName();

        location = transform.position;
    }

    public override string ToString()
    {
        return $"{gameObject.name} in {transform.parent.gameObject.name}";
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    NPCMovement npc = collision.GetComponent<NPCMovement>();


    //    if (npc != null)
    //    {
    //        //Debug.Log($"Landmark {ToString()} colliders with {collision.name}. Same level? NPC: {npc.currentLevel}, Landmark: {level}");

    //        npc.RegisterCollision(this);

            
    //    } 
    //}
}
