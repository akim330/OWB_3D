using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int id;

    public float speed = 10f;

    public NPCRole role;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private NPCDialogue _dialogue;
    private NPCInventory _inventory;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _dialogue = GetComponent<NPCDialogue>();
        //Debug.Log($"Getting dialogue: Is it null? {_dialogue == null}");

        _inventory = GetComponent<NPCInventory>();

        speed = 10f;
    }

    public NPCDialogue GetDialogue()
    {
        return _dialogue;
    }

    public NPCInventory GetInventory()
    {
        return _inventory;
    }
}
