using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DialogueInitiator : MonoBehaviour
{
    private List<DialoguePartner> currentDialogueBoxes;

    private DialoguePartner closestDialogue;
    private float closestDistance;

    private BoxCollider2D _collider;

    private DialoguePartner currentTalking;
    private bool inConvo;

    // Debug
    private bool debug;
    private StringBuilder sb;

    private void Start()
    {
        debug = false;

        currentDialogueBoxes = new List<DialoguePartner>();
        closestDistance = 99999f;

        _collider = GetComponent<BoxCollider2D>();

        sb = new StringBuilder();

        inConvo = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        DialoguePartner other = collision.GetComponentInChildren<DialoguePartner>();


        if (other != null)
        {
            other.HideTalkableBubble();
            currentDialogueBoxes.Remove(other);
            //UpdateClosest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entering trigger");

        DialoguePartner other = collision.GetComponent<DialoguePartner>();

        if (other != null)
        {
            if (debug)
            {
                Debug.Log($"Found a dialogue box! {other.gameObject.name}");
            }

            currentDialogueBoxes.Add(other);
            if (debug)
            {
                Debug.Log($"Adding a dialogue box: length {currentDialogueBoxes.Count}");
            }
        }
    }

    private void Update()
    {
        if (!inConvo)
        {
            // (1) Find closest
            DialoguePartner previousClosest = closestDialogue;
            closestDialogue = null;

            closestDistance = 99999f;

            sb.Clear();

            for (int i = 0; i < currentDialogueBoxes.Count; i++)
            {
                float currentDistance = Vector2.Distance(currentDialogueBoxes[i].transform.position, transform.position);
                if (currentDistance < closestDistance)
                {
                    closestDialogue = currentDialogueBoxes[i];
                    closestDistance = currentDistance;
                }
                sb.Append($"{currentDialogueBoxes[i].transform.name}: {currentDistance}\n");
            }
            if (closestDialogue != null)
            {
                sb.Append($"Closest: {closestDialogue.transform.name} at {closestDistance}\n");

            }

            if (previousClosest != null && previousClosest != closestDialogue)
            {
                previousClosest.HideTalkableBubble();
            }

            if (closestDialogue != null)
            {
                sb.Append("Showing the bubble");

                closestDialogue.ShowTalkableBubble();
            }
        }

        if (debug)
        {
            if (sb.ToString() == "")
            {
                Debug.Log("No current dialogue boxes");
            }
            else
            {
                Debug.Log(sb.ToString());
            }
        }

        // (2) Triggering dialogue
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (debug)
            {
                Debug.Log($"closestDialogue: {closestDialogue != null} | currentTalking: {currentTalking != null}");

            }

            if (closestDialogue == null) // Cancel current dialogue
            {
                if (currentTalking != null)
                {
                    Managers.Dialogue.EndConversation();
                }
            }
            else if (currentTalking == null)
            {
                Managers.Dialogue.StartConversation(this, closestDialogue);
                currentTalking = closestDialogue;
                inConvo = true;

                //closestDialogue.StartConversation(this);
                //currentTalking = closestDialogue;
            }
            else
            {
                Managers.Dialogue.ProgressConversation();
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    //}

    public void OnConversationEnd()
    {
        currentTalking = null;
        inConvo = false;
    }

}
