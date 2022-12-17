using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public List<ItemStack> itemStacks;

    public void Startup()
    {
        itemStacks = new List<ItemStack>();
    }

    public void AddItemStack(ItemStack itemStack)
    {
        itemStacks.Add(itemStack);
    }
}
 