using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private NPC npc;

    private void Awake()
    {
        npc = GetComponent<NPC>();
    }

    public Dialogue GetDialogue()
    {
        if (npc.role == NPCRole.None)
        {
            return new Dialogue(new string[] { "Testing dialogue manager", "Is it working?" });
        }
        else
        {
            return new Dialogue(new string[] { $"Hello, I am {npc.role}", "Is it working?" });
        }
    }
}
