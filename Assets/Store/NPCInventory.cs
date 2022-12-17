using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : MonoBehaviour
{
    public ItemStack[] itemStacks;

    // Start is called before the first frame update
    void Start()
    {
        // Testing by just setting the itemStacks manually
        itemStacks = new ItemStack[]
        {
            new ItemStack(Managers.Item.axe, 1),
            new ItemStack(Managers.Item.log, 10)

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
