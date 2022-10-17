using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemUser : MonoBehaviour
{
    [SerializeField] private GameObject _itemParent;
    [SerializeField] private ItemPool _pool;

    float timeBetweenSwings = 2.0f;
    float timer;

    private Item _equippedItem;
    public Item equippedItem
    {
        get { return _equippedItem; }
        set
        {
            if (value != _equippedItem)
            {
                ItemPrefabLabel[] childPrefabLabels = _itemParent.GetComponentsInChildren<ItemPrefabLabel>();
                int nActiveItems = childPrefabLabels.GetLength(0);

                if (nActiveItems > 1)
                {
                    Debug.LogError($"ERROR: Player has multiple items equipped ({nActiveItems}), which shouldn't happen!");
                    foreach (ItemPrefabLabel label in childPrefabLabels)
                    {
                        Debug.Log(label.gameObject.name);
                    }
                }
                else if (nActiveItems == 1)
                {
                    childPrefabLabels[0].gameObject.SetActive(false);
                    childPrefabLabels[0].transform.parent = _pool.transform;
                }

                _equippedItem = value;

                if (value != null)
                {
                    GameObject itemObject = _pool.GetObject(_equippedItem.itemName);
                    itemObject.SetActive(true);
                    itemObject.transform.parent = _itemParent.transform;
                    itemObject.transform.localPosition = new Vector3(0, 0, 0);
                    itemObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    itemObject.transform.localScale = new Vector3(1, 1, 1);
                }
                //equippedItem.prefab;
            }
        }
    }

    private Animator _animator;

    private void OnValidate()
    {
        _animator = GetComponent<Animator>();
    }

    public void EquipItem(Item item)
    {
        equippedItem = item;
    }

    private void Start()
    {
        timer = timeBetweenSwings;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] clipInfo;

        clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo[0].clip.name != "skin_swing")
        {
            _itemParent.SetActive(false);
            //Debug.Log($"Swing ended: Item parent active?: {_itemParent.activeSelf}");

        }
        else
        {
            _itemParent.SetActive(true);

        }

        //timer -= Time.deltaTime;

        //if (timer < 0)
        //{
        //    _itemParent.SetActive(true);
        //    _animator.SetTrigger("swing");
        //    timer = timeBetweenSwings;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            _itemParent.SetActive(true);
            //Debug.Log($"Swing started: Item parent active?: {_itemParent.activeSelf}");

            _animator.SetTrigger("swing");
        }

    }
}
