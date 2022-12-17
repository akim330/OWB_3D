using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt45 : MonoBehaviour
{
    [SerializeField] Camera _cam;

    private FollowCam _follow;

    // In degrees
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

        _follow = _cam.GetComponent<FollowCam>();

        angle = Mathf.Atan(Mathf.Abs(_follow._offset.y / _follow._offset.z)) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(angle, transform.rotation.y, transform.rotation.z);
    }

    private void Update()
    {
        angle = Mathf.Atan(Mathf.Abs(_follow._offset.y / _follow._offset.z)) * 180 / Mathf.PI;
        angle = 0;

        transform.eulerAngles = new Vector3(angle, _cam.transform.eulerAngles.y, transform.eulerAngles.z);

    }
}
