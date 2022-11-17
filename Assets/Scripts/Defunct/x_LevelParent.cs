using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x_LevelParent : MonoBehaviour
{
    //public Sprite[] sprites;
    //public Vector3[] positions;

    //public BuildingDoor[] doors;



    //public virtual void Deactivate()
    //{
    //    foreach (BuildingDoor door in doors)
    //    {
    //        door.gameObject.SetActive(false);
    //    }
    //}

    //public virtual void Activate()
    //{
    //    foreach (BuildingDoor door in doors)
    //    {
    //        door.gameObject.SetActive(true);
    //    }
    //}

    //public virtual void PopulateProperties()
    //{
    //    // Get all sprites
    //    SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

    //    sprites = new Sprite[renderers.Length];

    //    for (int i = 0; i < renderers.Length; i++)
    //    {
    //        sprites[i] = renderers[i].sprite;
    //    }

    //    // Get all positions

    //    Transform[] allTransforms = GetComponentsInChildren<Transform>();
    //    Transform[] transforms = new Transform[allTransforms.Length - 1];
    //    var j = 0;
    //    //Debug.Log($"number of all transforms: {allTransforms.Length}");
    //    //Debug.Log($"Parent ID: {GetInstanceID()}");
    //    foreach (Transform childTransform in allTransforms)
    //    {
    //        //Debug.Log($"Child ID: {childTransform.GetInstanceID()}");

    //        if (childTransform.GetInstanceID() != transform.GetInstanceID())
    //        {
    //            //Debug.Log($"Passed");

    //            transforms[j] = childTransform;
    //            j++;
    //        }
    //    }

    //    positions = new Vector3[transforms.Length];

    //    for (int i = 0; i < transforms.Length; i++)
    //    {
    //        positions[i] = transforms[i].position;
    //    }

    //    // Get all doors
    //    doors = GetComponentsInChildren<BuildingDoor>(true);
    //}
}
