using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorBuildingNode : BuildingNode
{
    private InteriorColliderParent[] _colliderParents;

    public InteriorBuildingNode(int level) : base(level)
    {
    }

    public override void Activate()
    {
        base.Activate();

        foreach (InteriorColliderParent parent in _colliderParents)
        {
            parent.gameObject.SetActive(true);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (InteriorColliderParent parent in _colliderParents)
        {
            parent.gameObject.SetActive(false);
        }
    }

    public override void PopulateProperties()
    {
        base.PopulateProperties();

        _colliderParents = GetComponentsInChildren<InteriorColliderParent>(true);
    }
}
