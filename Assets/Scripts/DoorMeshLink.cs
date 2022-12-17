using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorMeshLink : MonoBehaviour
{
    [SerializeField] private Door3D door;

    private NavMeshLink customLink;
    private OffMeshLink nativeLink;

    //private void OnValidate()
    //{
    //    door = GetComponentInParent<Door3D>();
    //    if (door == null)
    //    {
    //        Debug.LogError("Door not found!");
    //    }
    //}

    private void Awake()
    {
        customLink = GetComponent<NavMeshLink>();
        nativeLink = GetComponent<OffMeshLink>();
    }

    public void SetPoints()
    {
        if (customLink == null)
        {
            customLink = GetComponent<NavMeshLink>();
        }
        if (nativeLink == null)
        {
            nativeLink = GetComponent<OffMeshLink>();
        }

        nativeLink.enabled = false;
        nativeLink.enabled = true;

        customLink.startPoint = nativeLink.startTransform.TransformPoint(Vector3.zero);
        customLink.endPoint = nativeLink.endTransform.TransformPoint(Vector3.zero);
    }

    public void OpenDoor()
    {
        //Debug.Log("Opening door from DoorMeshLink!");
        if (!door.open)
        {
            //Debug.Log("Opening door from DoorMeshLink for real!");

            door.Operate();
        }
    }

    public void CloseDoor()
    {
        if (door.open)
        {
            door.Operate();
        }
    }
}
