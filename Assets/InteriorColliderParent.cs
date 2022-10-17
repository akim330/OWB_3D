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

public class InteriorColliderParent : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _west;
    [SerializeField] private BoxCollider2D _east;
    [SerializeField] private BoxCollider2D _north;
    [SerializeField] private BoxCollider2D _south;

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
}
