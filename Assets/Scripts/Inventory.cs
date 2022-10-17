using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private ItemSlot[] itemSlots;
    private ItemSlot selectedSlot;

    [SerializeField] private List<ItemStack> itemList = new List<ItemStack>();

    [SerializeField] private ItemUser player;

    public delegate bool OnItemPickUp(ItemStack itemStack);
    OnItemPickUp onItemPickUp;

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<ItemSlot>();
    }

    private void Start()
    {
        // Assign function to select actions
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].itemStack = null;
            itemSlots[i].OnToggleSelect += ChangeSelectedSlot;
        }

        // Set item slots
        //itemList = new List<Item>();

        OverwriteSlotsFromList();

    }

    public bool AddItem(ItemStack itemStack)
    {
        //Debug.Log($"itemSlots.Length: {itemSlots.Length}");
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //DEBUGPrintItemSlot(i);

            if (itemSlots[i].itemStack == null || itemSlots[i].itemStack.item == null)
            {
                itemSlots[i].itemStack = new ItemStack(itemStack.item, itemStack.count);
                //Debug.Log($"Adding item to slot {i}");
                //Debug.Log($"Inserted slot {i}: {itemSlots[i].itemStack.item.itemName} x{itemSlots[i].itemStack.count}");

                //DEBUGPrintItemSlot(0);

                return true;
            }
            else if (itemSlots[i].itemStack.item == itemStack.item)
            {
                if (itemSlots[i].itemStack.count + itemStack.count <= itemSlots[i].itemStack.item.maxCount)
                {
                    itemSlots[i].itemStack.count += itemStack.count;
                    return true;
                }
            }
        }
        //Debug.Log($"Couldn't add item");

        return false;
    }

    private void DEBUGPrintItemSlot(int i)
    {
        if (itemSlots[0] == null)
        {
            Debug.Log($"Slot {0}: null");
        }
        else
        {
            if (itemSlots[0].itemStack == null)
            {
                Debug.Log($"Slot {0}: null itemStack");

            }
            else
            {
                if (itemSlots[0].itemStack.item == null)
                {
                    Debug.Log($"Slot {0}: null item");

                }
                else
                {
                    Debug.Log($"Slot {0}: {itemSlots[0].itemStack.item.itemName} x{itemSlots[0].itemStack.count}");

                }
            }
        }
    }

    private void OverwriteSlotsFromList()
    {
        //Debug.Log("Overwriting!");
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < itemList.Count)
            {
                itemSlots[i].itemStack = itemList[i];
            }
            else
            {
                itemSlots[i].itemStack = null;
            }
        }
    }

    private void ChangeSelectedSlot(ItemSlot slot)
    {
        //Debug.Log("Change selected!");

        if (selectedSlot == null) // No slot selected yet
        {
            // Select this slot
            slot.Select();
            selectedSlot = slot;
            if (slot.itemStack != null)
            {
                player.equippedItem = slot.itemStack.item;
            }
        }
        else if (selectedSlot.slotNumber == slot.slotNumber) // This slot is already selected
        {
            // Deselect
            slot.Deselect();
            selectedSlot = null;
            player.equippedItem = null;

        }
        else // Other slot is selected
        {
            // Deselect current
            selectedSlot.Deselect();

            // Select this one
            slot.Select();
            selectedSlot = slot;

            if (slot.itemStack != null)
            {
                player.equippedItem = slot.itemStack.item;
            }
            else
            {
                player.equippedItem = null;
            }
        }
    }
}
