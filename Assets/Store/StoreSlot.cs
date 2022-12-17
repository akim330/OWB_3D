using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class StoreSlot : MonoBehaviour, IPointerClickHandler
{
    //public int slotNumber;
    public Action<StoreSlot> OnItemLeftClick;

    [SerializeField] private Image containerImage;
    //[SerializeField] private Image numberImage;
    [SerializeField] public Image itemImage;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private ItemStack _itemStack;
    public ItemStack itemStack
    {
        get { return _itemStack; }
        set
        {
            _itemStack = value;

            //if (_itemStack == null)
            //{
            //    Debug.Log($"Setting {gameObject.name} to null!");
            //}

            if (_itemStack == null || _itemStack.item == null)
            {
                //Debug.Log($"Setting {slotNumber} to null");
                itemImage.color = disabledColor;
                itemImage.sprite = null;
                hundredths.color = disabledColor;
                tens.color = disabledColor;
                ones.color = disabledColor;
            }
            else
            {
                itemImage.color = normalColor;
                Debug.Log($"Setting StoreSlot sprite: {_itemStack.item}");
                itemImage.sprite = _itemStack.item.Icon;
                if (_itemStack.item.maxCount == 1)
                {
                    hundredths.color = disabledColor;
                    tens.color = disabledColor;
                    ones.color = disabledColor;
                }
                else
                {
                    hundredths.color = normalColor;

                    ones.sprite = Resources.Load<Sprite>("Icons/count_" + _itemStack.count % 10);
                    ones.color = normalColor;

                    if (_itemStack.count >= 10)
                    {
                        tens.sprite = Resources.Load<Sprite>("Icons/count_" + Mathf.FloorToInt(_itemStack.count / 10) % 10);
                        tens.color = normalColor;

                        if (_itemStack.count >= 100)
                        {
                            hundredths.sprite = Resources.Load<Sprite>("Icons/count_" + Mathf.FloorToInt(_itemStack.count / 100) % 10);
                            hundredths.color = normalColor;

                        }
                        else
                        {
                            hundredths.color = disabledColor;
                        }

                    }
                    else
                    {
                        tens.color = disabledColor;
                        hundredths.color = disabledColor;
                    }
                }
            }
        }
    }

    [SerializeField] private Sprite defaultContainer;
    [SerializeField] private Sprite selectedContainer;

    private bool selected;

    public Action<StoreSlot> OnToggleSelect;

    // Count (bottom-right)

    [SerializeField] Image hundredths;
    [SerializeField] Image tens;
    [SerializeField] Image ones;

    public void UpdateCount()
    {
        if (_itemStack.item.maxCount == 1)
        {
            hundredths.color = disabledColor;
            tens.color = disabledColor;
            ones.color = disabledColor;
        }
        else
        {
            hundredths.color = normalColor;

            ones.sprite = Resources.Load<Sprite>("Icons/count_" + _itemStack.count % 10);
            ones.color = normalColor;

            if (_itemStack.count >= 10)
            {
                tens.sprite = Resources.Load<Sprite>("Icons/count_" + Mathf.FloorToInt(_itemStack.count / 10) % 10);
                tens.color = normalColor;

                if (_itemStack.count >= 100)
                {
                    hundredths.sprite = Resources.Load<Sprite>("Icons/count_" + Mathf.FloorToInt(_itemStack.count / 100) % 10);
                    hundredths.color = normalColor;

                }
                else
                {
                    hundredths.color = disabledColor;
                }

            }
            else
            {
                tens.color = disabledColor;
                hundredths.color = disabledColor;
            }
        }
    }

    private void Start()
    {
        selected = false;
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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnItemLeftClick != null)
            {
                OnItemLeftClick(this);
            }
        }
    }
}
