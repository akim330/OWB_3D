using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Vector3 _offset;


    //public float rotSpeed = 1.5f;
    //private float _rotY;

    //private void OnValidate()
    //{
    //    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

    //    if (objs.Length == 0)
    //    {
    //        Debug.LogError("No player found in the scene!");
    //    }
    //    else if (objs.Length > 1)
    //    {
    //        Debug.LogError("Multiple players found in the scene!");
    //    }
    //    else
    //    {
    //        target = objs[0].transform;
    //    }

    //}

    //private void Start()
    //{
    //    _rotY = transform.eulerAngles.y;
    //    _offset = target.position - transform.position;

    //    //transform.LookAt(target);

    //    //_offset = target.transform.position - transform.position;
    //}

    //// Update is called once per frame
    ////void Update()
    ////{
    ////    transform.position = target.transform.position - _offset; //new Vector3(target.position.x, target.position.y, target.position.z);
    ////    //transform.LookAt(target);

    ////}

    //private void LateUpdate()
    //{
    //    float horInput = Input.GetAxis("Horizontal");
    //    if (horInput != 0)
    //    {
    //        _rotY += horInput * rotSpeed;
    //    }
    //    else
    //    {
    //        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
    //    }

    //    Quaternion rotation = Quaternion.Euler(0, _rotY, 0);

    //    transform.position = target.position - (rotation * _offset);
    //    transform.LookAt(target);
    //}


    public float rotSpeed = 1.5f;

    private float _rotY;

    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        //_offset = target.position - transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //float horInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Z))
        {
            _rotY += rotSpeed;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            _rotY -= rotSpeed;
        }
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);

        transform.LookAt(target);

        //if (horInput != 0)
        //{
        //    _rotY += horInput * rotSpeed;
        //}
        //else
        //{
        //    //_rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        //}


    }
}
