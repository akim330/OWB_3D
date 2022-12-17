using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveWalls : MonoBehaviour
{
    [SerializeField] private Wall[] fronts;
    public Transform CameraPosition;

    public void FillFronts(Wall[] walls)
    {
        fronts = walls;
    }

    public void DissolveFront()
    {
        foreach (Wall front in fronts)
        {
            front.Dissolve();
        }
    }

    public void UndissolveFront()
    {
        foreach (Wall front in fronts)
        {
            front.Undissolve();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Managers.ExtInt.TransitionToInterior(this);
            DissolveFront();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Managers.ExtInt.TransitionToExterior(this);

            UndissolveFront();
        }
    }
}
