using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnValidate()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length == 0)
        {
            Debug.LogError("No player found in the scene!");
        }
        else if (objs.Length > 1)
        {
            Debug.LogError("Multiple players found in the scene!");
        }
        else
        {
            target = objs[0].transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
