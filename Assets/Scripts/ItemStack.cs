using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ItemStack
{

    [SerializeField] private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
        }
    }


    public int count;


    public ItemStack(Item _item, int _count)
    {
        //Debug.Log($"Setting item: {_item.name}");
        this.item = _item;
        this.count = _count;
    }

    public override string ToString()
    {
        if (item == null)
        {
            return $"null x{count}";
        }
        else
        {
            return $"{item.itemName} x{count}";

        }
    }

}
