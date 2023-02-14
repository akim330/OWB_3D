using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderMaker : MonoBehaviour
{

    [SerializeField] private string layerName = "bc";

    private void OnValidate()
    {
        Transform tileLayerTransform = transform.Find(layerName);

        foreach (Transform anchorTransform in tileLayerTransform)
        {
            Transform bakedParent = anchorTransform.Find("BakedParent");
            if (bakedParent != null)
            {
                foreach(Transform meshTransform in bakedParent)
                {
                    if (meshTransform.gameObject.GetComponent<MeshCollider>() == null)
                    {
                        meshTransform.gameObject.AddComponent(typeof(MeshCollider));
                    }
                }
            }
        }
    }

}
