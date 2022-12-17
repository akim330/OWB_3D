using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using Consequences;

[CreateAssetMenu]
public class ChoiceDialogueNode : DialogueNode
{


    public Tuple<string, int, Consequence >[] choices;



    private StringBuilder sb;
    public override string ToString()
    {
        sb = new StringBuilder();
        sb.Clear();

        sb.Append($"Choice Node {idx}: \"{text}\"\n");
        foreach(Tuple<string, int, Consequence> tuple in choices)
        {
            sb.Append($"\t* \"{tuple.Item1}\" to Node {tuple.Item2}");
        }
        return sb.ToString();
    }
}
