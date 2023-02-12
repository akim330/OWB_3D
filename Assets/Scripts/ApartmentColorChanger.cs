using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ApartmentColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private MeshRenderer[] allRenderers;


    private void OnValidate()
    {
        Material chosen = materials[Random.Range(0, materials.Length)];

        foreach(MeshRenderer _renderer in allRenderers)
        {
            _renderer.material = chosen;
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
