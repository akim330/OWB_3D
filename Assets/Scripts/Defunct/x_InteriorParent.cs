using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x_InteriorParent : x_LevelParent
{
    //private InteriorColliderParent[] _colliderParents;

    //public override void Deactivate()
    //{
    //    base.Deactivate();

    //    DeactivateColliders();
    //}

    //public override void Activate()
    //{
    //    base.Activate();

    //    ActivateColliders();
    //}

    //public override void PopulateProperties()
    //{
    //    base.PopulateProperties();

    //    // Get interior collider parent
    //    _colliderParents = GetComponentsInChildren<InteriorColliderParent>(true);


    //}

    //public void ActivateColliders()
    //{
    //    for (int i = 0; i < _colliderParents.Length; i++)
    //    {
    //        _colliderParents[i].gameObject.SetActive(true);
    //        if (i == 0)
    //        {
    //            _colliderParents[i].ActivateColliders(
    //                new ColliderPosition[] {
    //                    ColliderPosition.East,
    //                    ColliderPosition.South,
    //                    ColliderPosition.West
    //                });
    //        }
    //        else if (i == _colliderParents.Length - 1)
    //        {
    //            _colliderParents[i].ActivateColliders(
    //                new ColliderPosition[] {
    //                    ColliderPosition.East,
    //                    ColliderPosition.North,
    //                    ColliderPosition.West
    //                });
    //        }
    //        else
    //        {
    //            _colliderParents[i].ActivateColliders(
    //                new ColliderPosition[] {
    //                    ColliderPosition.East,
    //                    ColliderPosition.West
    //                });
    //        }
    //    }
    //}

    //public void DeactivateColliders()
    //{
    //    for (int i = 0; i < _colliderParents.Length; i++)
    //    {
    //        _colliderParents[i].gameObject.SetActive(false);
    //    }
    //}
}
