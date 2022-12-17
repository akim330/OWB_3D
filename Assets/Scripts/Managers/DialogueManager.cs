using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Ink.Runtime;
using Consequences;

//public class Dialogue
//{
//    public string[] lines;

//    public Dialogue(string[] _lines)
//    {
//        lines = _lines;
//    }
//}

public class DialogueManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private DialogueBox dialogueBox;
    private DialoguePartner currentPartner;
    private DialogueInitiator currentInitiator;

    //private NPCDialogue currentNPCDialogue;
    private DialogueTree currentDialogueTree;
    private DialogueNode currentNode;

    [Header("Choices")]
    private ChoiceDialogueNode currentChoiceNode;
    private bool choicesShowing;
    private int currentChoice;
    //private Dialogue currentDialogue;
    //private int currentLine;

    private Dictionary<int, NPCDialogue> npcDialogueDict;

    public Dictionary<string, string> globalDialogueVariables;

    private bool debug;



    //private Story currentStory;

    public void Startup()
    {
        debug = false;

        npcDialogueDict = new Dictionary<int, NPCDialogue>();

        globalDialogueVariables = new Dictionary<string, string>();
        globalDialogueVariables.Add("town", "Test Town");

    }

    public void LogNPCDialogue(NPC npc)
    {
        //Debug.Log($"Is npcDialogue null? {npc.GetDialogue() == null}");
        npcDialogueDict.Add(npc.id, npc.GetDialogue());
    }

    //public DialogueTree GetDialogue(int characterID)
    //{
    //    currentNPCDialogue = npcDialogueDict[characterID];

    //    return currentNPCDialogue.GetDialogue();
    //}

    public void EndConversation()
    {
        dialogueBox.HideBox();
        currentDialogueTree = null;
        currentInitiator.OnConversationEnd();
        currentPartner.OnEndConversation();
    }

    public void StartConversation(DialogueInitiator initiator, DialoguePartner partner)
    {
        currentInitiator = initiator;
        currentPartner = partner;

        // Notify initiator that conversation has started (so e.g. they can freeze movement)
        initiator.OnStartConversation();

        // Set dialogue name
        dialogueBox.SetName($"Person {partner.id}");

        // Show dialogue box
        dialogueBox.ShowBox();

        // Get lines and give them to the dialogue box
        currentDialogueTree = currentPartner.GetDialogue();
        if (debug)
        {
            Debug.Log(currentDialogueTree.ToString());
        }
        currentNode = currentDialogueTree.GetFirstNode();

        // Notify partner that they're being talked to
        partner.OnStartConversation(initiator);

        // Output to dialogue box
        OutputCurrentDialogueNode();

    }

    public void ProgressConversation()
    {
        if (choicesShowing)
        {
            currentNode = currentDialogueTree.SubmitChoice(currentChoice);
        }
        else
        {
            currentNode = currentDialogueTree.Continue();
        }

        if (currentNode == null)
        {
            EndConversation();
        }
        else
        {
            OutputCurrentDialogueNode();
        }
        //if (currentLine == currentDialogue.lines.Length)
        //{
        //    EndConversation();
        //}
        //else
        //{
        //    //dialogueBox.SetLine(currentDialogue.lines[currentLine]);
        //    currentLine++;
        //}
    }

    private void OutputCurrentDialogueNode()
    {
        if (currentNode is StaticDialogueNode)
        {
            currentChoiceNode = null;
            choicesShowing = false;
            currentChoice = -1;
            dialogueBox.SetStaticDialogueNode(currentNode as StaticDialogueNode);
        }
        else if (currentNode is ChoiceDialogueNode)
        {
            currentChoiceNode = currentNode as ChoiceDialogueNode;
            choicesShowing = true;
            currentChoice = 0;
            dialogueBox.SetChoiceDialogueNode(currentNode as ChoiceDialogueNode);
            dialogueBox.UpdateChoiceArrow(currentChoice);

        }

        else
        {
            Debug.LogError("Not implemented!");
        }
    }

    private void Update()
    {
        if (choicesShowing)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currentChoice < currentChoiceNode.choices.Length - 1)
                {
                    currentChoice++;
                    dialogueBox.UpdateChoiceArrow(currentChoice);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentChoice > 0)
                {
                    currentChoice--;
                    dialogueBox.UpdateChoiceArrow(currentChoice);
                }

            }
        }
    }

    public NPC GetCurrentNPC()
    {
        return currentPartner.npc;
    }


    // [GRAVEYARD 1] PRE-DIALOGUE TREE

    //public void Startup()
    //{
    //    npcDialogueDict = new Dictionary<int, NPCDialogue>();

    //    globalDialogueVariables = new Dictionary<string, string>();
    //    globalDialogueVariables.Add("town", "Test Town");

    //}

    //public void LogNPCDialogue(NPC npc)
    //{
    //    Debug.Log($"Is npcDialogue null? {npc.GetDialogue() == null}");
    //    npcDialogueDict.Add(npc.id, npc.GetDialogue());
    //}

    //public Dialogue GetDialogue(int characterID)
    //{
    //    currentNPCDialogue = npcDialogueDict[characterID];

    //    return currentNPCDialogue.GetDialogue();
    //}

    //public void EndConversation()
    //{
    //    dialogueBox.HideBox();
    //    currentDialogue = null;
    //    currentInitiator.OnConversationEnd();
    //    currentPartner.OnEndConversation();
    //}

    //public void StartConversation(DialogueInitiator initiator, DialoguePartner partner)
    //{
    //    currentInitiator = initiator;
    //    currentPartner = partner;

    //    // Set dialogue name
    //    dialogueBox.SetName($"Person {partner.id}");

    //    // Show dialogue box
    //    dialogueBox.ShowBox();

    //    // Get lines and give them to the dialogue box
    //    currentDialogue = GetDialogue(partner.id);
    //    dialogueBox.SetLine(currentDialogue.lines[0]);

    //    // Set current line number
    //    currentLine = 1;

    //    // Notify partner that they're being talked to
    //    partner.OnStartConversation(initiator);
    //}

    //public void ProgressConversation()
    //{
    //    if (currentLine == currentDialogue.lines.Length)
    //    {
    //        EndConversation();
    //    }
    //    else
    //    {
    //        dialogueBox.SetLine(currentDialogue.lines[currentLine]);
    //        currentLine++;
    //    }
    //}



    // [GRAVEYARD 2] INK
    //public void StartInkConversation(DialogueInitiator initiator, DialoguePartner partner, TextAsset inkJSON)
    //{
    //    currentInitiator = initiator;
    //    currentPartner = partner;

    //    // Set dialogue name
    //    dialogueBox.SetName($"Person {partner.id}");

    //    // Show dialogue box
    //    dialogueBox.ShowBox();

    //    // Notify partner that they're being talked to
    //    // partner.OnStartConversation(initiator);

    //    // Set lines
    //    Debug.Log($"Starting ink convo");
    //    currentStory = new Story(inkJSON.text);

    //    ProgressInkConversation();
    //}

    //public void ProgressInkConversation()
    //{
    //    if (currentStory.canContinue)
    //    {
    //        Debug.Log($"Continuing ink convo");

    //        dialogueBox.SetLine(currentStory.Continue());
    //    }
    //    else
    //    {
    //        EndConversation();
    //    }
    //}
}
