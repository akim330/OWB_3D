using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorParent : MonoBehaviour
{
    private InteriorColliderParent[] _colliderParents;
    public Sprite[] sprites { get; private set; }
    public Vector3[] positions { get; private set; }

    public void PopulateProperties()
    {
        // Get all sprites
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        sprites = new Sprite[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            sprites[i] = renderers[i].sprite;
        }

        // Get all positions

        Transform[] allTransforms = GetComponentsInChildren<Transform>();
        Transform[] transforms = new Transform[allTransforms.Length - 1];
        var j = 0;
        //Debug.Log($"number of all transforms: {allTransforms.Length}");
        //Debug.Log($"Parent ID: {GetInstanceID()}");
        foreach(Transform childTransform in allTransforms)
        {
            //Debug.Log($"Child ID: {childTransform.GetInstanceID()}");

            if (childTransform.GetInstanceID() != transform.GetInstanceID())
            {
                //Debug.Log($"Passed");

                transforms[j] = childTransform;
                j++;
            }
        }

        positions = new Vector3[transforms.Length];

        for (int i = 0; i < transforms.Length; i++)
        {
            positions[i] = transforms[i].position;
        }

        // Get interior collider parent
        _colliderParents = GetComponentsInChildren<InteriorColliderParent>(true);

        //Debug.Log($"Populating properties! {sprites.Length}, {positions.Length}, {_colliderParent == null}");

    }

    public void ActivateColliders()
    {
        for (int i = 0; i < _colliderParents.Length; i++)
        {
            _colliderParents[i].gameObject.SetActive(true);
            if (i == 0)
            {
                _colliderParents[i].ActivateColliders(
                    new ColliderPosition[] {
                        ColliderPosition.East,
                        ColliderPosition.South,
                        ColliderPosition.West
                    });
            }
            else if (i == _colliderParents.Length - 1)
            {
                _colliderParents[i].ActivateColliders(
                    new ColliderPosition[] {
                        ColliderPosition.East,
                        ColliderPosition.North,
                        ColliderPosition.West
                    });
            }
            else
            {
                _colliderParents[i].ActivateColliders(
                    new ColliderPosition[] {
                        ColliderPosition.East,
                        ColliderPosition.West
                    });
            }
        }
    }

    public void DeactivateColliders()
    {
        for (int i = 0; i < _colliderParents.Length; i++)
        {
            _colliderParents[i].gameObject.SetActive(false);
        }
    }
}
