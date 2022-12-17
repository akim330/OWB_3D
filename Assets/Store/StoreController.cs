using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] private StorePanel _panel;

    private bool _visible;

    private void Start()
    {
        HidePanel();
    }

    public void HidePanel()
    {
        _panel.gameObject.SetActive(false);
        _visible = false;
    }

    public void ShowPanel()
    {
        _panel.gameObject.SetActive(true);
        _panel.LoadNPCInventory();
        _visible = true;
    }

    public void TogglePanel()
    {
        if (_visible)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }
}
