using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private int power;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();

        //Debug.Log($"Collision with {collider}");
        if (collider.gameObject.layer == LayerMask.NameToLayer("Choppable"))
        {
            //Debug.Log("Collision with choppable!");


            ChoppableObject obj = collider.GetComponent<ChoppableObject>();
            if (obj != null)
            {
                obj.OnHit(power);
            }
            else
            {
                Debug.LogError($"{collider} has layer Choppable but no ChoppableObject component!");
            }
        }
    }
}
