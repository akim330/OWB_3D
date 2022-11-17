using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private Image _boxImage;
    [SerializeField] private TextMeshProUGUI _boxText;
    [SerializeField] private Image _nameImage;
    [SerializeField] private TextMeshProUGUI _nameText;

    // Start is called before the first frame update
    void Start()
    {
        HideBox();
    }

    public void HideBox()
    {
        _boxImage.gameObject.SetActive(false);
        _nameImage.gameObject.SetActive(false);
    }

    public void ShowBox()
    {
        _boxImage.gameObject.SetActive(true);
        _nameImage.gameObject.SetActive(true);
    }

    public void SetLine(string s)
    {
        _boxText.text = s;
    }

    public void SetName(string s)
    {
        _nameText.text = s;
    }
}
