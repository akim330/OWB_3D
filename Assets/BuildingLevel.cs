using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevel : MonoBehaviour
{
    public int levelNum;

    [SerializeField] private Wall floor;
    [SerializeField] private Wall left;
    [SerializeField] private Wall right;
    [SerializeField] private Wall back;
    public Wall front;
    [SerializeField] private Wall roof;

    public Transform cameraPosition;

    public void DissolveFloor()
    {
        floor.Dissolve();
    }

    public void DissolveRoof()
    {
        roof.Dissolve();
    }
}
