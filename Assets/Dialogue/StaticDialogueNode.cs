using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Consequences;

[CreateAssetMenu]
public class StaticDialogueNode : DialogueNode
{
    public int nextIdx;

    public Consequence consequence;

    private StringBuilder sb;
    public override string ToString()
    {
        return $"Static Node {idx}: \"{text}\" to Node {nextIdx}";

        //sb = new StringBuilder();
        //sb.Clear();

        //sb.Append($"Choice Node {idx}: \"{text}\"\n");
        //foreach (Tuple<string, int> tuple in choices)
        //{
        //    sb.Append($"\t* \"{tuple.Item1}\" to Node {tuple.Item2}");
        //}
        //return sb.ToString();
    }
}
