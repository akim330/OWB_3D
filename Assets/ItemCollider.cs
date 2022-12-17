using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{

    private Axe axe;

    private void Awake()
    {
        axe = GetComponentInParent<Axe>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Collider collider = collision.GetComponent<Collider2D>();

        Debug.Log($"Collision with {collider}");
        if (collider.gameObject.layer == LayerMask.NameToLayer("Choppable"))
        {
            Debug.Log("Collision with choppable!");


            ChoppableObject obj = collider.GetComponent<ChoppableObject>();
            if (obj != null)
            {
                obj.OnHit(axe.power);
            }
            else
            {
                Debug.LogError($"{collider} has layer Choppable but no ChoppableObject component!");
            }
        }
    }
}
