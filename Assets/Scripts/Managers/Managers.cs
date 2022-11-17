using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FILL #1: Add new manager
[RequireComponent(typeof(ItemManager))]
[RequireComponent(typeof(CursorManager))]
[RequireComponent(typeof(TownManager))]
[RequireComponent(typeof(PaletteManager))]
[RequireComponent(typeof(BuildingManager))]
[RequireComponent(typeof(DialogueManager))]
[RequireComponent(typeof(ExtIntManager))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(NPCManager))]
[RequireComponent(typeof(BiomeManager))]
[RequireComponent(typeof(MoneyManager))]

public class Managers : MonoBehaviour
{
    private List<IGameManager> _startSequence;

    // FILL #2: Add new manager
    public static ItemManager Item;
    public static CursorManager Cursor;
    public static TownManager Town;
    public static PaletteManager Palette;
    public static BuildingManager Building;
    public static DialogueManager Dialogue;
    public static ExtIntManager ExtInt;
    public static TimeManager Time;
    public static NPCManager NPC;
    public static BiomeManager Biome;
    public static MoneyManager Money;

    void Awake()
    {
        // FILL #3: Add new manager
        Item = GetComponent<ItemManager>();
        Cursor = GetComponent<CursorManager>();
        Town = GetComponent<TownManager>();
        Palette = GetComponent<PaletteManager>();
        Building = GetComponent<BuildingManager>();
        Dialogue = GetComponent<DialogueManager>();
        ExtInt = GetComponent<ExtIntManager>();
        Time = GetComponent<TimeManager>();
        NPC = GetComponent<NPCManager>();
        Biome = GetComponent<BiomeManager>();
        Money = GetComponent<MoneyManager>();

        // FILL #4: Add new manager
        _startSequence = new List<IGameManager>();
        _startSequence.Add(Item);
        _startSequence.Add(Cursor);
        _startSequence.Add(Town);
        _startSequence.Add(Palette);
        _startSequence.Add(Building);
        _startSequence.Add(Dialogue);
        _startSequence.Add(ExtInt);
        _startSequence.Add(Time);
        _startSequence.Add(NPC);
        _startSequence.Add(Biome);
        _startSequence.Add(Money);

        StartCoroutine(StartupManagers());
    }

    IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            // Recount the number of ready managers
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
            }
            yield return null;
        }

        Debug.Log("All managers started up");
    }
}
