using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOperator : MonoBehaviour
{
    public float radius = 3f;

    private GameObject interiorObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            Debug.Log($"Trying to open nearby doors");

            foreach (Collider2D collider in colliders)
            {
                //Debug.Log(collider.name);

                BuildingDoor exteriorDoor = collider.GetComponent<BuildingDoor>();

                if (exteriorDoor != null)
                {
                    Managers.ExtInt.ToInterior(exteriorDoor.buildingParent);
                }

                InteriorBuildingDoor interiorDoor = collider.GetComponent<InteriorBuildingDoor>();

                if (interiorDoor != null)
                {
                    Debug.Log($"Found InteriorBuildingDoor");


                    if (exteriorDoor != null)
                    {
                        Debug.LogError("Colliding with exterior door and interior door at the same time, which shouldn't happen!");
                    }

                    Managers.ExtInt.ToExterior();
                }

                //Vector3 direction = collider.transform.position - transform.position;
                //if (Vector3.Dot(direction, transform.forward) > 0.5f)
                //{
                //    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                //}
            }
        }

    }
}
