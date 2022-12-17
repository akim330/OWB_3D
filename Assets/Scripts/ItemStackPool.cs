using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropStyle {
    Throw,
    Loot
}

public enum DropDirection
{
    Left,
    Right
}

public class ItemStackPool : MonoBehaviour
{
    private Queue<ItemStackObject> _itemStackObjects;


    private void OnValidate()
    {
        _itemStackObjects = new Queue<ItemStackObject>();

        ItemStackObject[] children = GetComponentsInChildren<ItemStackObject>(includeInactive: true);

        foreach (ItemStackObject child in children)
        {
            _itemStackObjects.Enqueue(child);
            child.gameObject.SetActive(false);
        }
    }

    public GameObject GetItemStackObject()
    {
        return _itemStackObjects.Dequeue().gameObject;
    }

    public void Reabsorb(ItemStackObject itemStackObject)
    {
        itemStackObject.Reset();
        itemStackObject.transform.parent = transform;
        itemStackObject.gameObject.SetActive(false);
        _itemStackObjects.Enqueue(itemStackObject);
    }

    public void DropItem(Item item, int count, Vector3 position, DropStyle style, float direction)
    {
        Vector2 dropForce;

        if (style == DropStyle.Throw)
        {
            dropForce = new Vector2(10f * direction, 20f);
        }
        else if (style == DropStyle.Loot)
        {
            float xDirection = Random.Range(-5f, 5f);
            dropForce = new Vector2(xDirection * direction, 10f);
        }
        else
        {
            dropForce = new Vector2(0f, 0f);
            Debug.LogError($"ERROR: Not implemented DropStyle {style}");
        }

        //Debug.Log($"Dropping with force: {dropForce} and direction: {direction} with total: {dropForce * direction}");


        GameObject newObject = GetItemStackObject();
        newObject.SetActive(true);
        newObject.transform.parent = null;
        newObject.transform.position = position;
        newObject.GetComponent<Rigidbody>().AddForce(dropForce, ForceMode.Impulse);

        ItemStackObject itemStackObject = newObject.GetComponent<ItemStackObject>();

        itemStackObject.itemStack = new ItemStack(item, count);
        //Debug.Log($"Setting item to {item.itemName}");

        if (style == DropStyle.Throw)
        {
            newObject.GetComponentInChildren<ItemStackTrigger>().OnDrop();
            itemStackObject.OnThrow();
        }
    }
}
