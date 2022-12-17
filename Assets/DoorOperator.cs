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
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider collider in colliders)
            {
                Door3D door = collider.GetComponent<Door3D>();

                if (door != null)
                {
                    door.Operate();
                    
                }

                //InteriorBuildingDoor interiorDoor = collider.GetComponent<InteriorBuildingDoor>();

                //if (interiorDoor != null)
                //{
                //    Debug.Log($"Found InteriorBuildingDoor");


                //    if (exteriorDoor != null)
                //    {
                //        Debug.LogError("Colliding with exterior door and interior door at the same time, which shouldn't happen!");
                //    }

                //    Managers.ExtInt.ToExterior();
                //}

                //Vector3 direction = collider.transform.position - transform.position;
                //if (Vector3.Dot(direction, transform.forward) > 0.5f)
                //{
                //    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                //}
            }
        }

    }

    private bool LayerMatch(string playerLayer, string doorLayer)
    {
        if (playerLayer == "Player" && doorLayer == "Default")
        {
            return true;
        }
        if (playerLayer == "Level1Player" && doorLayer == "Level1Colliders")
        {
            return true;
        }
        if (playerLayer == "Level2Player" && doorLayer == "Level2Colliders")
        {
            return true;
        }
        return false;
    }
}
