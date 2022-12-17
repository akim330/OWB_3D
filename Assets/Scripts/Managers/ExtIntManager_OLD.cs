using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ExtIntManager_OLD : MonoBehaviour
{
    //public ManagerStatus status { get; private set; }

    //[SerializeField] private SpriteMask[] _masks;
    //private Sprite[] _sprites;
    //private Vector2[] _positions;

    //[SerializeField] private Camera _cam;
    //[SerializeField] private GameObject _cameraCover;
    //[SerializeField] private CinemachineVirtualCamera _exteriorCam;
    //[SerializeField] private CinemachineVirtualCamera _interiorCam;

    //[SerializeField] private GameObject _player;

    ////private int interiorCamZ = 20;
    ////private int interiorPlayerZ = 21;
    //private int exteriorCamZ = -20;
    //private int exteriorPlayerZ = -10;

    //private BuildingNode _activeNode;

    //private bool debug;

    //public void Startup()
    //{
    //    debug = false;
    //}


    //public void TransitionToNode(BuildingNode node)
    //{
    //    if (debug)
    //    {
    //        Debug.Log($"Player going to node {node.ToString()}");
    //    }
    //    //buildingParent.TransitionToLevel(level);

    //    if (_activeNode != null)
    //    {
    //        _activeNode.Deactivate();
    //    }
    //    _activeNode = node;
    //    node.Activate();

    //    _sprites = node.sprites;
    //    _positions = node.positions;

    //    // (1) Set sprite masks 

    //    if (node.level == 0)
    //    {
    //        // GOING TO EXTERIOR: Set sprite masks to null
    //        for (int i = 0; i < _masks.Length; i++)
    //        {
    //            _masks[i].sprite = null;
    //        }
    //    }
    //    else
    //    {
    //        // GOING TO INTERIOR: Set sprite masks to current interior shape (so that it appears through the camera cover)
    //        if (_sprites.Length > _masks.Length)
    //        {
    //            Debug.LogError($"ERROR: Need more masks. Only have {_masks.Length} but need {_sprites.Length}");
    //        }

    //        for (int i = 0; i < _masks.Length; i++)
    //        {
    //            if (i < _sprites.Length)
    //            {
    //                //Debug.Log($"Setting mask {i}");
    //                _masks[i].sprite = _sprites[i];
    //                _masks[i].transform.position = _positions[i]; //node.transform.TransformPoint(_positions[i]);
    //                _masks[i].transform.position = new Vector3(_masks[i].transform.position.x, _masks[i].transform.position.y, node.level * 10 + 2);

    //            }
    //            else
    //            {
    //                _masks[i].sprite = null;
    //            }
    //        }

    //    }

    //    // (2) Camera cover
    //    if (node.level == 0)
    //    {
    //        // EXTERIOR: Deactivate camera cover
    //        _cameraCover.SetActive(false);
    //    }
    //    else
    //    {
    //        // INTERIOR: Activate camera cover
    //        _cameraCover.SetActive(true);
    //    }

    //    // (3) Camera
    //    if (node.level == 0)
    //    {
    //        // EXTERIOR: Activate exterior virtual camera 
    //        _exteriorCam.gameObject.SetActive(true);

    //        // EXTERIOR: Set main camera Z position so that it's before the town level
    //        _cam.transform.position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, exteriorCamZ);
    //        _cam.GetComponent<FollowCam>().enabled = false;
    //    }
    //    else
    //    {
    //        // INTERIOR: Inactivate exterior virtual camera so that we're just using the main camera (so we can control the Z position)
    //        _exteriorCam.gameObject.SetActive(false);

    //        //INTERIOR:  Set main camera Z position so that it's beyond the town level
    //        _cam.transform.position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, node.level * 10);
    //        _cam.GetComponent<FollowCam>().enabled = true;

    //    }

    //    // (4) Player
    //    if (node.level == 0)
    //    {
    //        // EXTERIOR: Move player so that they're beyond town level
    //        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, exteriorPlayerZ);
    //        _player.gameObject.layer = LayerMask.NameToLayer($"Player");

    //    }
    //    else
    //    {
    //        // INTERIOR: Move player so that they're before town level
    //        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, (node.level + 1) * 10 - 1);
    //        _player.gameObject.layer = LayerMask.NameToLayer($"Level{node.level}Player");

    //    }
    //}

}
