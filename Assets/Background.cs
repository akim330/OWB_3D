using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private float _offset;

    //[SerializeField] private Vector3 rotationTarget = new Vector3(0, 1, 1);
    [SerializeField] private float yDisplace = 10;
    private Quaternion _rot;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //_rot = Quaternion.FromToRotation(Vector3.forward, rotationTarget);

        transform.position = _cam.transform.position +  _cam.transform.forward * _offset;
        transform.position = new Vector3(transform.position.x, transform.position.y + yDisplace, transform.position.z);
        transform.LookAt(_cam.transform);
    }
}
