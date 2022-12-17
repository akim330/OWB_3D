using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanel : MonoBehaviour
{
    public StoreSlot[] storeSlots;
    private StoreSlot selectedSlot;

    [SerializeField] CartItemSlot cartSlot;

    [SerializeField] private ItemStack[] itemList;

    private NPCInventory _currentNPCInventory;
    private NPCInventory currentNPCInventory {
        get { return _currentNPCInventory; }
        set
        {
            _currentNPCInventory = value;
            //Debug.Log($"Setting storeSlots: {storeSlots.Length}, {_currentNPCInventory.itemStacks.Length}");
            for (int i = 0; i < storeSlots.Length; i++)
            {
                if (i < _currentNPCInventory.itemStacks.Length)
                {
                    //Debug.Log($"Setting storeSlot: {_currentNPCInventory.itemStacks[i].item}");

                    storeSlots[i].itemStack = _currentNPCInventory.itemStacks[i];

                }
            }
        }
    }

    //[SerializeField] private ItemUser player;

    //public delegate bool OnItemPickUp(ItemStack itemStack);
    //OnItemPickUp onItemPickUp;

    private void OnEnable()
    {
        foreach (StoreSlot slot in storeSlots)
        {
            slot.OnItemLeftClick += SelectSlot;
        }
    }

    private void SelectSlot(StoreSlot slot)
    {
        if (selectedSlot == null)
        {
            selectedSlot = slot;
            selectedSlot.Select();

            cartSlot.itemStack = slot.itemStack;
        }
        else if (selectedSlot == slot)
        {
            selectedSlot.Deselect();
        }
        else
        {
            selectedSlot.Deselect();
            selectedSlot = slot;
            selectedSlot.Select();

            cartSlot.itemStack = slot.itemStack;
        }


    }

    public void BuyItem()
    {
        if (selectedSlot != null)
        {
            Managers.Inventory.AddItemStack(selectedSlot.itemStack);
            selectedSlot.itemStack = null;
            selectedSlot.Deselect();
            cartSlot.itemStack = null;
        }
        else
        {
            Debug.Log("WARNING: Trying to buy item but none selected");
        }
    }

    private void OnValidate()
    {
        storeSlots = GetComponentsInChildren<StoreSlot>();
    }

    private void Awake()
    {

        // Assign function to select actions
        for (int i = 0; i < storeSlots.Length; i++)
        {
            storeSlots[i].itemStack = null;
            storeSlots[i].OnToggleSelect += ChangeSelectedSlot;
        }

        // Set item slots
        //itemList = new List<Item>();

        //OverwriteSlotsFromList();

    }

    public bool AddItem(ItemStack itemStack)
    {
        //Debug.Log($"itemSlots.Length: {itemSlots.Length}");
        for (int i = 0; i < storeSlots.Length; i++)
        {
            //DEBUGPrintItemSlot(i);

            if (storeSlots[i].itemStack == null || storeSlots[i].itemStack.item == null)
            {
                storeSlots[i].itemStack = new ItemStack(itemStack.item, itemStack.count);
                //Debug.Log($"Adding item to slot {i}");
                //Debug.Log($"Inserted slot {i}: {itemSlots[i].itemStack.item.itemName} x{itemSlots[i].itemStack.count}");

                //DEBUGPrintItemSlot(0);

                return true;
            }
            else if (storeSlots[i].itemStack.item == itemStack.item)
            {
                if (storeSlots[i].itemStack.count + itemStack.count <= storeSlots[i].itemStack.item.maxCount)
                {
                    storeSlots[i].itemStack.count += itemStack.count;
                    storeSlots[i].UpdateCount();
                    return true;
                }
            }
        }
        //Debug.Log($"Couldn't add item");

        return false;
    }

    private void DEBUGPrintItemSlot(int i)
    {
        if (storeSlots[0] == null)
        {
            Debug.Log($"Slot {0}: null");
        }
        else
        {
            if (storeSlots[0].itemStack == null)
            {
                Debug.Log($"Slot {0}: null itemStack");

            }
            else
            {
                if (storeSlots[0].itemStack.item == null)
                {
                    Debug.Log($"Slot {0}: null item");

                }
                else
                {
                    Debug.Log($"Slot {0}: {storeSlots[0].itemStack.item.itemName} x{storeSlots[0].itemStack.count}");

                }
            }
        }
    }

    private void OverwriteSlotsFromList()
    {
        //Debug.Log("Overwriting!");
        for (int i = 0; i < storeSlots.Length; i++)
        {
            if (i < itemList.Length)
            {
                storeSlots[i].itemStack = itemList[i];
            }
            else
            {
                storeSlots[i].itemStack = null;
            }
        }
    }

    private void ChangeSelectedSlot(StoreSlot slot)
    {
        //Debug.Log("Change selected!");

        if (selectedSlot == null) // No slot selected yet
        {
            // Select this slot
            slot.Select();
            selectedSlot = slot;
        }
        else if (selectedSlot == slot) // This slot is already selected
        {
            // Deselect
            slot.Deselect();
            selectedSlot = null;
            //player.equippedItem = null;

        }
        else // Other slot is selected
        {
            // Deselect current
            selectedSlot.Deselect();

            // Select this one
            slot.Select();
            selectedSlot = slot; 

            //if (slot.itemStack != null)
            //{
            //    player.equippedItem = slot.itemStack.item;
            //}
            //else
            //{
            //    player.equippedItem = null;
            //}
        }
    }

    public void LoadNPCInventory()
    {
        currentNPCInventory = Managers.Dialogue.GetCurrentNPC().GetInventory();

    }
}
