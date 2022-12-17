using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt3D : MonoBehaviour
{
    private MeshRenderer _mesh;
    //private Material _material;
    [SerializeField] private Shader shader;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;

    [SerializeField] private float fractionOfPI;

    private Vector3 skewDirection;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.SetFloat("_MinY", _mesh.bounds.min.y);

    }

    // Update is called once per frame
    void Update()
    {
        _mesh.material.SetFloat("_Angle", fractionOfPI * Mathf.PI);

        //skewDirection = new Vector3(
        //    player.position.x - cam.transform.position.x,
        //    0,
        //    player.position.z - cam.transform.position.z
        //);

        skewDirection = new Vector3(
            transform.position.x - cam.transform.position.x,
            0,
            transform.position.z - cam.transform.position.z
        );

        skewDirection = Vector3.Normalize(skewDirection);
        //Debug.Log($"transform: {transform.position} | cam: {cam.transform.position} | skew: {skewDirection}");
        _mesh.material.SetVector("_SkewDirection", skewDirection);
    }
}
