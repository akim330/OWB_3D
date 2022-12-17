using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3D : MonoBehaviour
{
    [SerializeField] BoxCollider leftDoor;
    [SerializeField] BoxCollider rightDoor;

    public bool open;

    private void Start()
    {
        open = false;

        if (leftDoor != null)
        {
            leftDoor.transform.localPosition = new Vector3(leftDoor.transform.localPosition.x, leftDoor.transform.localPosition.y, -0.01f);
        }
        if (rightDoor != null)
        {
            rightDoor.transform.localPosition = new Vector3(rightDoor.transform.localPosition.x, rightDoor.transform.localPosition.y, -0.01f);

        }
    }

    public void Operate()
    {
        Debug.Log($"Operating door");
        float multiplier;
        if (open)
        {
            multiplier = -1;
        }
        else
        {
            multiplier = 1;
        }
        //Debug.Log($"left: {leftDoor.bounds.extents} | right: {rightDoor.bounds.extents}");
        if (leftDoor != null)
        {
            leftDoor.transform.position = new Vector3(
                leftDoor.transform.position.x - multiplier * leftDoor.bounds.extents.x * 2,
                leftDoor.transform.position.y,
                leftDoor.transform.position.z
            );
        }

        if (rightDoor != null)
        {
            rightDoor.transform.position = new Vector3(
                rightDoor.transform.position.x + multiplier * rightDoor.bounds.extents.x * 2,
                rightDoor.transform.position.y,
                rightDoor.transform.position.z
            );
        }

        open = !open;
    }
}
