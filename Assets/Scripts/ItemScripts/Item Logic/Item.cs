using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite Icon;
    public int maxCount;

    [SerializeField] private GameObject prefab;

    [System.NonSerialized] public PolygonCollider2D collider;

    private void OnValidate()
    {
        Icon = Resources.Load<Sprite>("Icons/" + itemName);
        prefab = Resources.Load ("Prefabs/" + itemName) as GameObject;
        collider = prefab.GetComponent<PolygonCollider2D>();
    }
}
