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

    public void Startup()
    {
    }

    public Dialogue GetDialogue(int characterID)
    {
        return new Dialogue(new string[] { "Testing dialogue manager", "Is it working?" });
    }
}
