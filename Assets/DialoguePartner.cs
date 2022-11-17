using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePartner : MonoBehaviour
{
    private bool _talking;

    private SpriteRenderer _renderer;
    private Color disabledColor = new Color(1, 1, 1, 0);
    public int id;

    private DialogueInitiator currentIntiator;

    private NPCMovement _npcMovement;


    [SerializeField] private SpriteRenderer _talkableBubbleRenderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        id = GetComponent<NPC>().id;
        _npcMovement = GetComponent<NPCMovement>();

        HideTalkableBubble();
    }

    public void ShowTalkableBubble()
    {
        if (!_talking)
        {
            //Debug.Log($"{transform.parent.name}: Showing bubble");

            _talkableBubbleRenderer.color = Color.white;
        }
    }

    public void HideTalkableBubble()
    {
        //Debug.Log($"{transform.parent.name}: Hiding bubble");

        _talkableBubbleRenderer.color = disabledColor;

    }

    public void OnStartConversation(DialogueInitiator initiator)
    {
        currentIntiator = initiator;
        _npcMovement.OnStartConversation(initiator.transform);
    }

    public void OnEndConversation()
    {
        currentIntiator = null;
        _npcMovement.OnEndConversation();
    }
}
