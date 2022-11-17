using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DialogueInitiatorOLD : MonoBehaviour
{
    //private List<DialogueBox> currentDialogueBoxes;

    //private DialogueBox closestDialogue;
    //private float closestDistance;
    //private DialogueBox currentTalking;

    //private BoxCollider2D _collider;

    //// Debug
    //private StringBuilder sb;

    //private void Start()
    //{
    //    currentDialogueBoxes = new List<DialogueBox>();
    //    closestDistance = 99999f;

    //    _collider = GetComponent<BoxCollider2D>();

    //    sb = new StringBuilder();
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    DialogueBox other = collision.GetComponentInChildren<DialogueBox>();


    //    if (other != null)
    //    {
    //        other.HideTalkableBubble();
    //        currentDialogueBoxes.Remove(other);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    DialogueBox other = collision.GetComponentInChildren<DialogueBox>();

    //    if (other != null)
    //    {
    //        currentDialogueBoxes.Add(other);
    //    }
    //}

    //private void Update()
    //{
    //    // (1) Find closest
    //    DialogueBox previousClosest = closestDialogue;
    //    closestDialogue = null;

    //    closestDistance = 99999f;

    //    sb.Clear();

    //    for (int i = 0; i < currentDialogueBoxes.Count; i++)
    //    {
    //        float currentDistance = Vector2.Distance(currentDialogueBoxes[i].transform.parent.position, transform.position);
    //        if (currentDistance < closestDistance)
    //        {
    //            closestDialogue = currentDialogueBoxes[i];
    //            closestDistance = currentDistance;
    //        }
    //        sb.Append($"{currentDialogueBoxes[i].transform.parent.name}: {currentDistance}\n");
    //    }
    //    if (closestDialogue != null)
    //    {
    //        sb.Append($"Closest: {closestDialogue.transform.parent.name} at {closestDistance}\n");

    //    }

    //    if (previousClosest != null && previousClosest != closestDialogue)
    //    {
    //        previousClosest.HideTalkableBubble();
    //    }

    //    if (closestDialogue != null)
    //    {
    //        sb.Append("Showing the bubble");

    //        closestDialogue.ShowTalkableBubble();
    //    }

    //    // (2) Triggering dialogue
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        if (closestDialogue == null) // Cancel current dialogue
    //        {
    //            if (currentTalking != null)
    //            {
    //                currentTalking.EndConversation();
    //            }
    //        }
    //        else if (currentTalking == null)
    //        {
    //            closestDialogue.StartConversation(this);
    //            currentTalking = closestDialogue;
    //        }
    //        else
    //        {
    //            closestDialogue.ProgressConversation();
    //        }
    //    }
    //}

    //public void OnConversationEnd()
    //{
    //    currentTalking = null;
    //}

}
