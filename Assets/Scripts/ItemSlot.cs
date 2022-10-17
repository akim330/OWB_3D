using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemSlot : MonoBehaviour
{
    public int slotNumber;

    [SerializeField] private Image containerImage;
    [SerializeField] private Image numberImage;
    [SerializeField] private Image itemImage;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private ItemStack _itemStack;
    public ItemStack itemStack
    {
        get { return _itemStack; }
        set
        {
            _itemStack = value;

            if (_itemStack == null || _itemStack.item == null)
            {
                //Debug.Log($"Setting {slotNumber} to null");
                itemImage.color = disabledColor;
                itemImage.sprite = null;
            }
            else
            {
                itemImage.color = normalColor;
                itemImage.sprite = _itemStack.item.Icon;
            }
        }
    }

    private Sprite defaultContainer;
    private Sprite selectedContainer;

    private KeyCode key;

    private bool selected;

    public Action<ItemSlot> OnToggleSelect;

    private ItemStackPool _itemStackPool;
    private PlayerMovement _player;

    private void OnValidate()
    {
        _itemStackPool = FindObjectOfType<ItemStackPool>();
        _player = FindObjectOfType<PlayerMovement>();

        Image[] images = GetComponentsInChildren<Image>();
        containerImage = images[0];
        numberImage = images[1];
        itemImage = images[2];

        numberImage.sprite = Resources.Load<Sprite>("Icons/itemslot_" + slotNumber);

        defaultContainer = Resources.Load<Sprite>("Icons/itemslot");
        selectedContainer = Resources.Load<Sprite>("Icons/itemslot_selected");

        if (slotNumber == 0)
        {
            key = KeyCode.Alpha0;
        }
        else if (slotNumber == 1)
        {
            key = KeyCode.Alpha1;
        }
        else if (slotNumber == 2)
        {
            key = KeyCode.Alpha2;
        }
        else if (slotNumber == 3)
        {
            key = KeyCode.Alpha3;
        }
        else if (slotNumber == 4)
        {
            key = KeyCode.Alpha4;
        }
        else if (slotNumber == 5)
        {
            key = KeyCode.Alpha5;
        }
        else if (slotNumber == 6)
        {
            key = KeyCode.Alpha6;
        }
        else if (slotNumber == 7)
        {
            key = KeyCode.Alpha7;
        }
        else if (slotNumber == 8)
        {
            key = KeyCode.Alpha8;
        }
        else if (slotNumber == 9)
        {
            key = KeyCode.Alpha9;
        }
    }

    private void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if dropped
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _itemStackPool.DropItem(itemStack.item, itemStack.count, _player.transform.position, style : DropStyle.Throw, direction : _player.transform.localScale.x);
                itemStack = null;

                //GameObject newStackObject = _itemStackPool.GetItemStackObject();
                //newStackObject.SetActive(true);
                //newStackObject.transform.parent = null;
                //newStackObject.transform.position = _player.transform.position;
                //newStackObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 20f) * _player.transform.localScale.x, ForceMode2D.Impulse);

                //ItemStack newItemStack = newStackObject.GetComponent<ItemStack>();
                //newItemStack.item = itemStack.item;
                //newItemStack.count = itemStack.count;

                //newStackObject.GetComponentInChildren<ItemStackTrigger>().OnDrop();

                //itemStack = null;

            }
        }

        // Check if toggle select
        if (Input.GetKeyDown(key))
        {
            OnToggleSelect(this);
        }
    }

    public void Deselect()
    {
        containerImage.sprite = defaultContainer;
        selected = false;
    }

    public void Select()
    {
        containerImage.sprite = selectedContainer;
        selected = true;
    }
}
