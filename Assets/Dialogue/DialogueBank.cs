using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Consequences;


public class DialogueBank : MonoBehaviour
{
    public static DialogueBank instance { get; private set; }

    public DialogueTree A1;

    private void Awake()
    {
        instance = this;
        //if (instance != null && instance != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    instance = this;
        //}

        CreateNodes();
    }

    private void CreateNodes()
    {
        A1 = ScriptableObject.CreateInstance<DialogueTree>();
        A1.nodes = new DialogueNode[]{
            new StaticDialogueNode()
            {
                idx = 0,
                textRegex = @"Hello, my name is $name$.",
                text = "",
                nextIdx = 1,
                consequence = null
            },

            new StaticDialogueNode()
            {
                idx = 1,
                textRegex = @"Welcome to $town$",
                text = "",
                nextIdx = 2,
                consequence = null
            },

            new ChoiceDialogueNode()
            {
                idx = 2,
                textRegex = @"Which pokemon is your favorite?",
                text = "",
                choices = new Tuple<string, int, Consequence>[]
                {
                    new Tuple<string, int, Consequence>("Squirtle", -1, () => Debug.Log($"You chose Squirtle")),
                    new Tuple<string, int, Consequence>("Bulbasaur", -1, TestMake("Bulbasaur").Invoke),
                    new Tuple<string, int, Consequence>("Charmander", -1, TestMake2("Charmander")),

                }
            }
        };
    }

    private Action TestMake(string s)
    {
        return () => Debug.Log($"Hey you chose {s}");
    }

    private Consequence TestMake2(string s)
    {
        return new Consequence(() => Debug.Log($"Did you choose {s}?"));
    }

}
