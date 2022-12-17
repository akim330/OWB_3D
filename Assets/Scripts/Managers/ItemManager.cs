using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public Item axe;
    public Item pickaxe;
    public Item log;

    public void Startup()
    {
    }
}
