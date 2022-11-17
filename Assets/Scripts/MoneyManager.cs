using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] MoneyTracker tracker;

    private int _money;
    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            tracker.UpdateMoney(money);
        }
    }

    public void Startup()
    {
        money = 0;
    }
    
}
