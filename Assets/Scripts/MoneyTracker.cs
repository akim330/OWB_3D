using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour
{
    [SerializeField] Image tenThousands;
    [SerializeField] Image thousands;
    [SerializeField] Image hundreds;
    [SerializeField] Image tens;
    [SerializeField] Image ones;

    private Color normalColor = new Color(1, 1, 1, 1);
    private Color disabledColor = new Color(1, 1, 1, 0);

    public void UpdateMoney(int amount)
    {
        ones.sprite = Resources.Load<Sprite>("Icons/count_g_" + amount % 10);
        ones.color = normalColor;

        if (amount >= 10)
        {
            tens.sprite = Resources.Load<Sprite>("Icons/count_g_" + Mathf.FloorToInt(amount / 10) % 10);
            tens.color = normalColor;

            if (amount >= 100)
            {
                hundreds.sprite = Resources.Load<Sprite>("Icons/count_g_" + Mathf.FloorToInt(amount / 100) % 10);
                hundreds.color = normalColor;

                if (amount >= 1000)
                {
                    thousands.sprite = Resources.Load<Sprite>("Icons/count_g_" + Mathf.FloorToInt(amount / 1000) % 10);
                    thousands.color = normalColor;

                    if (amount >= 10000)
                    {
                        tenThousands.sprite = Resources.Load<Sprite>("Icons/count_g_" + Mathf.FloorToInt(amount / 10000) % 10);
                        tenThousands.color = normalColor;
                    }
                    else
                    {
                        tenThousands.color = disabledColor;
                    }
                }
                else
                {
                    thousands.color = disabledColor;
                    tenThousands.color = disabledColor;
                }
            }
            else
            {
                hundreds.color = disabledColor;
                thousands.color = disabledColor;
                tenThousands.color = disabledColor;
            }

        }
        else
        { 
            tens.color = disabledColor;
            hundreds.color = disabledColor;
            thousands.color = disabledColor;
            tenThousands.color = disabledColor;
        }        

    }
}
