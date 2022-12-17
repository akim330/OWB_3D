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
    [SerializeField] private Image _tradeImage;

    [Header("Choices")]
    private ChoiceDialogueNode currentChoiceNode;
    [SerializeField] private ChoiceBox[] choiceBoxes;

    [SerializeField] private RectTransform choiceArrowTransform;

    //private bool choicesShowing;
    //private int currentChoice;

    // Start is called before the first frame update
    void Start()
    {
        HideBox();
    }

    public void HideBox()
    {
        _boxImage.gameObject.SetActive(false);
        _nameImage.gameObject.SetActive(false);
        _tradeImage.gameObject.SetActive(false);

        foreach(ChoiceBox box in choiceBoxes)
        {
            box.gameObject.SetActive(false);
        }
        choiceArrowTransform.gameObject.SetActive(false);
    }

    public void ShowBox()
    {
        _boxImage.gameObject.SetActive(true);
        _nameImage.gameObject.SetActive(true);
        _tradeImage.gameObject.SetActive(true);

    }

    public void SetName(string s)
    {
        _nameText.text = s;
    }

    public void SetStaticDialogueNode(StaticDialogueNode node)
    {
        _boxText.text = node.text;

        currentChoiceNode = null;
    }

    public void SetChoiceDialogueNode(ChoiceDialogueNode node)
    {
        _boxText.text = node.text;

        for (int i = 0; i < choiceBoxes.Length; i++)
        {
            if (i < node.choices.Length)
            {
                //Debug.Log($"Filling choice {i}: {choiceBoxes[i] == null}, {currentChoiceNode.choices[i] == null}");
                choiceBoxes[i].gameObject.SetActive(true);
                choiceBoxes[i].choiceTextUI.text = node.choices[i].Item1;
            }
            else
            {
                choiceBoxes[i].gameObject.SetActive(false);
            }
        }
        choiceArrowTransform.gameObject.SetActive(true);

        //choicesShowing = true;
        //currentChoice = 0;
        //UpdateChoiceArrow();
    }

    public void UpdateChoiceArrow(int currentChoice)
    {
        RectTransform choiceRectTransform = choiceBoxes[currentChoice].GetComponent<RectTransform>();

        choiceArrowTransform.position = new Vector3(choiceArrowTransform.position.x, choiceRectTransform.rect.center.y + choiceRectTransform.position.y, 0);

        //Debug.Log($"currentChoice: {currentChoice} on {choiceRectTransform.gameObject.name}\n" +
        //    $"choiceArrowTransform: {choiceArrowTransform.position}\n" +
        //    $"choiceArrowTransform: {choiceArrowTransform.position}\n"
        //);

        //Debug.Log($"currentChoice: {currentChoice} \n" +
        //    $"choiceArrowTransform: {choiceArrowTransform.position}\n" +
        //    $"choiceArrowTransform anchored: {choiceArrowTransform.anchoredPosition}\n" +
        //    $"choiceArrowTransform pivot: {choiceArrowTransform.pivot}\n" +
        //    $"choiceBoxTransform: {choiceBoxes[currentChoice].GetComponent<RectTransform>().position}\n" +
        //    $"choiceBoxTransform anchored: {choiceBoxes[currentChoice].GetComponent<RectTransform>().anchoredPosition}\n" +
        //    $"choiceBoxTransform pivot: {choiceBoxes[currentChoice].GetComponent<RectTransform>().pivot}\n"
        //);
    }

    //public void SetLine(string s)
    //{
    //    Debug.Log($"Setting dialogue: {s}");

    //    _boxText.text = s;
    //}
}
