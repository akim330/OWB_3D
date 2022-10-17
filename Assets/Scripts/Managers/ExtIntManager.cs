using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExtIntManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private SpriteMask[] _masks;
    private Sprite[] _sprites;
    private Vector3[] _positions;

    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _cameraCover;
    [SerializeField] private CinemachineVirtualCamera _exteriorCam;
    [SerializeField] private CinemachineVirtualCamera _interiorCam;

    [SerializeField] private GameObject _player;

    private BuildingDoor[] _exteriorDoors;
    private InteriorBuildingDoor[] _interiorDoors;

    //private GameObject interiorObject;
    //private InteriorColliderParent interiorColliderParent;

    private InteriorParent _currentInterior;

    private int interiorCamZ = 40;
    private int interiorPlayerZ = 41;
    private int exteriorCamZ = -20;
    private int exteriorPlayerZ = -10;

    public void Startup()
    {
    }

    public void ToInterior(BuildingParent buildingParent) 
    {
        Debug.Log($"Going to interior! {buildingParent.name}");

        _currentInterior = buildingParent.gameObject.GetComponentInChildren<InteriorParent>();

        // Set sprite mask to current interior shape (so that it appears through the camera cover)
        _sprites = _currentInterior.sprites;
        _positions = _currentInterior.positions;

        if (_sprites.Length > _masks.Length)
        {
            Debug.LogError($"ERROR: Need more masks. Only have {_masks.Length} but need {_sprites.Length}");
        }

        for (int i = 0; i < _masks.Length; i++)
        {
            if (i < _sprites.Length)
            {
                //Debug.Log($"Setting mask {i}");
                _masks[i].sprite = _sprites[i];
                _masks[i].transform.position = _currentInterior.transform.TransformPoint(_positions[i]);
            }
            else
            {
                _masks[i].sprite = null;
            }
        }        

        // Activate camera cover
        _cameraCover.SetActive(true);

        // Inactivate exterior virtual camera so that we're just using the main camera (so we can control the Z position)
        _exteriorCam.gameObject.SetActive(false);

        // Set main camera Z position so that it's beyond the town level
        _cam.transform.position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, interiorCamZ);

        // Move player so that they're beyond town level
        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, interiorPlayerZ);

        // Activate interior collider walls
        _currentInterior.ActivateColliders();

        // Set doors
        _exteriorDoors = buildingParent.GetComponentsInChildren<BuildingDoor>(true);
        _interiorDoors = buildingParent.GetComponentsInChildren<InteriorBuildingDoor>(true);

        foreach(BuildingDoor door in _exteriorDoors)
        {
            door.gameObject.SetActive(false);
        }

        foreach(InteriorBuildingDoor door in _interiorDoors)
        {
            door.gameObject.SetActive(true);
        }
    }

    public void ToExterior()
    {
        Debug.Log("Going to exterior!");

        // Set sprite mask to current interior shape (so that it appears through the camera cover)
        for (int i = 0; i < _masks.Length; i++)
        {
            _masks[i].sprite = null;
        }

        // Deactivate camera cover
        _cameraCover.SetActive(false);

        // Activate exterior virtual camera 
        _exteriorCam.gameObject.SetActive(true);

        // Set main camera Z position so that it's beyond the town level
        _cam.transform.position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, exteriorCamZ);

        // Move player so that they're beyond town level
        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, exteriorPlayerZ);

        // Deactivate interior collider walls
        _currentInterior.DeactivateColliders();

        // Set doors
        foreach (BuildingDoor door in _exteriorDoors)
        {
            door.gameObject.SetActive(true);
        }

        foreach (InteriorBuildingDoor door in _interiorDoors)
        {
            door.gameObject.SetActive(false);
        }
    }
}
