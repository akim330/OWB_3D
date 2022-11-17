using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string[] lines;

    public Dialogue(string[] _lines)
    {
        lines = _lines;
    }
}

public class DialogueManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private DialogueBox dialogueBox;
    private DialoguePartner currentPartner;
    private DialogueInitiator currentInitiator;

    private NPCDialogue currentNPCDialogue;
    private Dialogue currentDialogue;
    private int currentLine;

    private Dictionary<int, NPCDialogue> npcDialogueDict;

    public void Startup()
    {
        npcDialogueDict = new Dictionary<int, NPCDialogue>();

    }

    public void LogNPCDialogue(NPC npc)
    {
        Debug.Log($"Is npcDialogue null? {npc.GetDialogue() == null}");
        npcDialogueDict.Add(npc.id, npc.GetDialogue());
    }

    public Dialogue GetDialogue(int characterID)
    {
        currentNPCDialogue = npcDialogueDict[characterID];

        return currentNPCDialogue.GetDialogue();
    }

    public void EndConversation()
    {
        dialogueBox.HideBox();
        currentDialogue = null;
        currentInitiator.OnConversationEnd();
        currentPartner.OnEndConversation();
    }

    public void StartConversation(DialogueInitiator initiator, DialoguePartner partner)
    {
        currentInitiator = initiator;
        currentPartner = partner;

        // Set dialogue name
        dialogueBox.SetName($"Person {partner.id}");

        // Show dialogue box
        dialogueBox.ShowBox();

        // Get lines and give them to the dialogue box
        currentDialogue = GetDialogue(partner.id);
        dialogueBox.SetLine(currentDialogue.lines[0]);

        // Set current line number
        currentLine = 1;

        // Notify partner that they're being talked to
        partner.OnStartConversation(initiator);
    }

    public void ProgressConversation()
    {
        if (currentLine == currentDialogue.lines.Length)
        {
            EndConversation();
        }
        else
        {
            dialogueBox.SetLine(currentDialogue.lines[currentLine]);
            currentLine++;
        }
    }
}
