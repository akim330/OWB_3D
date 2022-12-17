using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private MeshRenderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>(true);
    }

    public void Dissolve()
    {
        //Debug.Log("Dissolving walls");
        foreach(MeshRenderer renderer in _renderers)
        {
            renderer.enabled = false;
        }
    }

    public void Undissolve()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.enabled = true;
        }
    }
}
