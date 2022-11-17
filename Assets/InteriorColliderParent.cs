using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderPosition
{
    West,
    East,
    North,
    South
}

public enum BuildingStackPosition
{
    Bottom,
    Middle,
    Top,
    Sole
}


public class InteriorColliderParent : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _west;
    [SerializeField] private BoxCollider2D _east;
    [SerializeField] private BoxCollider2D _north;
    [SerializeField] private BoxCollider2D _south;

    private void Awake()
    {
        int interiorColliderLayer = LayerMask.NameToLayer("InteriorColliders");
        gameObject.layer = interiorColliderLayer;
        _west.gameObject.layer = interiorColliderLayer;
        _east.gameObject.layer = interiorColliderLayer;
        _north.gameObject.layer = interiorColliderLayer;
        _south.gameObject.layer = interiorColliderLayer;
    }

    public void CullColliders(BuildingStackPosition position)
    {
        if (position == BuildingStackPosition.Bottom)
        {
            Destroy(_north.gameObject);
        }
        else if (position == BuildingStackPosition.Middle)
        {
            Destroy(_north.gameObject);
            Destroy(_south.gameObject);
        }
        else if (position == BuildingStackPosition.Top)
        {
            Destroy(_south.gameObject);
        }
        else if (position == BuildingStackPosition.Sole)
        {
        }
        else
        {
            Debug.LogError($"Unimplemented branch: {position}");
        }
    }

    public void ActivateColliders(ColliderPosition[] positions)
    {
        foreach (ColliderPosition position in positions)
        {
            if (position == ColliderPosition.West)
            {
                _west.gameObject.SetActive(true);
            }
            else if (position == ColliderPosition.East)
            {
                _east.gameObject.SetActive(true);
            }
            else if (position == ColliderPosition.North)
            {
                _north.gameObject.SetActive(true);
            }
            else if (position == ColliderPosition.South)
            {
                _south.gameObject.SetActive(true);
            }
        }
    }

    public void SetLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
        _west.gameObject.layer = gameObject.layer;
        _east.gameObject.layer = gameObject.layer;
        _north.gameObject.layer = gameObject.layer;
        _south.gameObject.layer = gameObject.layer;
    }
}
