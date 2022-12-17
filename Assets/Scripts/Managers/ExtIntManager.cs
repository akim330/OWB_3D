using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ExtIntManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private CinemachineVirtualCamera interiorCam;
    [SerializeField] private CinemachineVirtualCamera exteriorCam;


    public void Startup()
    {
    }

    public void TransitionToInterior(DissolveWalls interior)
    {
        // Place interior camera
        interiorCam.transform.position = interior.CameraPosition.position;
        interiorCam.transform.LookAt(interior.transform.position);

        // Change camera priorities so interiorCam is shown
        interiorCam.Priority = 2;
        exteriorCam.Priority = 1;
    }

    public void TransitionToExterior(DissolveWalls exterior)
    {
        // Change camera priorities so exteriorCam is shown
        interiorCam.Priority = 1;
        exteriorCam.Priority = 2;
        exteriorCam.transform.position = interiorCam.transform.position;
    }
}
