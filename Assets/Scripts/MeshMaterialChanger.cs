using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeshMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private Mesh[] meshes;


    [SerializeField] private MeshRenderer[] allMeshRenderers;
    [SerializeField] private MeshFilter[] allMeshFilters;

    private void OnValidate()
    {
        // Choose random mesh (random # of floors)
        Mesh chosenMesh = meshes[Random.Range(0, meshes.Length)];

        foreach (MeshFilter filter in allMeshFilters)
        {
            filter.mesh = chosenMesh;
        }

        Material chosenMaterial = materials[Random.Range(0, materials.Length)];

        foreach(MeshRenderer _renderer in allMeshRenderers)
        {
            _renderer.material = chosenMaterial;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
