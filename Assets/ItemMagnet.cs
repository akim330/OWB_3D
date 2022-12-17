using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    [SerializeField] private float radius;

    private SphereCollider _collider;

    // Update is called once per frame
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = radius;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            collision.GetComponentInParent<ItemStackObject>()?.FlyToTransform(transform.parent);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
