using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private int power;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();

        //Debug.Log($"Collision with {collider}");
        if (collider.gameObject.layer == LayerMask.NameToLayer("Mineable"))
        {
            //Debug.Log("Collision with choppable!");


            MineableObject obj = collider.GetComponent<MineableObject>();
            if (obj != null)
            {
                obj.OnHit(power);
            }
            else
            {
                Debug.LogError($"{collider} has layer Mineable but no Mineable Object component!");
            }
        }
    }
}
