using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStackTrigger : MonoBehaviour
{
    [SerializeField] private ItemStackObject _itemStackObject;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemStackPool _pool;

    private float dropCoolDown = 1.0f;
    private float timer;
    private bool justDropped;

    private void OnValidate()
    {
        if (_inventory == null)
        {
            _inventory = FindObjectOfType<Inventory>();
        }
        if (_pool == null)
        {
            _pool = FindObjectOfType<ItemStackPool>();
        }
        if (_itemStackObject == null)
        {
            _itemStackObject = GetComponentInParent<ItemStackObject>();
        }
    }

    private void Start()
    {
        _itemStackObject = GetComponentInParent<ItemStackObject>();
    }

    private void Update()
    {
        if (justDropped)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                justDropped = false;
            }
        }
    }

    public void OnDrop()
    {
        justDropped = true;
        timer = dropCoolDown;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!justDropped)
        {
            //Debug.Log($"Triggered entered by {collider.tag}!");

            if (collider.tag == "Player")
            {
                //Debug.Log($"It's player!");

                //Debug.Log("Player colliding with item");

                if (_inventory.AddItem(_itemStackObject.itemStack))
                {
                    _pool.Reabsorb(_itemStackObject);
                }
            }
        }
        else
        {
            //Debug.Log("Collided but just dropped!!!");
        }
    }
}
