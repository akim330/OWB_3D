using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] private Dictionary<string, GameObject> itemDict;


    private void OnValidate()
    {
        itemDict = new Dictionary<string, GameObject>();

        ItemPrefabLabel[] children = GetComponentsInChildren<ItemPrefabLabel>(includeInactive: true);

        foreach (ItemPrefabLabel child in children)
        {
            itemDict.Add(child.itemName, child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    public GameObject GetObject(string itemName)
    {
        return itemDict[itemName];
    }
}
