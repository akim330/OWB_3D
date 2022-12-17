using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private NPC npc;

    private Dictionary<string, string> varDict;

    private void Awake()
    {
        npc = GetComponent<NPC>();

        varDict = new Dictionary<string, string>();
        varDict.Add("name", "Billy");
    }

    public DialogueTree GetDialogue()
    {
        DialogueTree tree = DialogueBank.instance.A1;
        tree.FillRegex(varDict);
        return tree;
    }

    //public Dialogue GetDialogue()
    //{
    //    if (npc.role == NPCRole.None)
    //    {
    //        return new Dialogue(new string[] { "Testing dialogue manager", "Is it working?" });
    //    }
    //    else
    //    {
    //        return new Dialogue(new string[] { $"Hello, I am {npc.role}", "Is it working?" });
    //    }
    //}
}
