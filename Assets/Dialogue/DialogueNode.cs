using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : ScriptableObject
{
    public int idx;
    public string textRegex;
    public string text;

    public override string ToString()
    {
        return $"Node {idx}: \"{text}\"";
    }
}
