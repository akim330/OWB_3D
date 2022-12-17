using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;

[CreateAssetMenu]
public class DialogueTree : ScriptableObject
{
    static readonly Regex re = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);

    public DialogueNode[] nodes;

    private int currentIdx;

    private Dictionary<int, DialogueNode> nodeDict;

    private Dictionary<string, string> currentMatchDict;

    private void Awake()
    {
        currentIdx = 0;
    }

    public DialogueNode GetFirstNode()
    {
        return nodes[0];
    }

    public DialogueNode SubmitChoice(int currentChoice)
    {
        ChoiceDialogueNode choiceNode = nodes[currentIdx] as ChoiceDialogueNode;
        int nextIdx = choiceNode.choices[currentChoice].Item2;

        // Call Consequence
        choiceNode.choices[currentChoice].Item3?.Invoke();
        if (nextIdx == -1)
        {
            return null;
        }
        else
        {
            return nodes[nextIdx];
        }
    }

    public DialogueNode Continue()
    {
        StaticDialogueNode staticNode = nodes[currentIdx] as StaticDialogueNode;

        // Call Consequence
        staticNode.consequence?.Invoke();

        if (staticNode.nextIdx == -1)
        {
            return null;
        }
        else
        {
            currentIdx = staticNode.nextIdx;
            return nodes[currentIdx];

        }

    }

    private string MatchEvaluator(Match match)
    {
        if (currentMatchDict.ContainsKey(match.Groups[1].Value))
        {
            return currentMatchDict[match.Groups[1].Value];
        }
        else if (Managers.Dialogue.globalDialogueVariables.ContainsKey(match.Groups[1].Value))
        {
            return Managers.Dialogue.globalDialogueVariables[match.Groups[1].Value];
        }
        else
        {
            Debug.Log($"MatchEvaluator Error: key {match.Groups[1].Value} not found");
            return null;
        }
    }

    public void FillRegex(Dictionary<string, string> dict)
    {
        currentMatchDict = dict;

        foreach (DialogueNode node in nodes)
        {
            node.text = re.Replace(node.textRegex, MatchEvaluator);
        }
    }

    public void ResetIndex()
    {
        currentIdx = 0;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Clear();

        foreach(DialogueNode node in nodes)
        {
            sb.Append(node.ToString());
            sb.Append("\n");
        }
        return sb.ToString();
    }

}

