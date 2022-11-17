using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCRole
{
    Lumberjack,
    Farmer,
    Blacksmith,
    Grocer,
    None
}

public class NPCManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] GameObject basePersonPrefab;
    [SerializeField] AnimatorOverrideController[] npcOverrides;

    int nPeople;

    public void Startup()
    {
        nPeople = 0;
    }

    private string Level2NPCLayer(int level)
    {
        if (level == 0)
        {
            return "NPC";
        }
        else if (level == 1)
        {
            return "Level1NPC";
        }
        else if (level == 2)
        {
            return "Level2NPC";
        }
        else
        {
            Debug.LogError("Not implemented!");
            return null;
        }
    }

    private void RandomizeNPC(NPC npc, NPCRole role)
    {
        BodyGenerator bodyGenerator = npc.GetComponent<BodyGenerator>();

        // Random gender / age
        bodyGenerator.npcOverride = npcOverrides[Random.Range(0, npcOverrides.Length)];

        // Assign role
        npc.role = role;

        // Set ID 
        npc.id = nPeople;

       
    }

    public void PlaceNPCInNodeBlock(NPCRole role, BuildingNodeBlock nodeBlock)
    {
        Vector3 nodeWorldPosition = nodeBlock.transform.TransformPoint(new Vector3(2, 2, 0));

        GameObject npcObj = Instantiate(basePersonPrefab, new Vector3(nodeWorldPosition.x, nodeWorldPosition.y, nodeWorldPosition.z - 1), Quaternion.Euler(Vector3.zero));
        npcObj.name = $"Person{nPeople.ToString()}";
        npcObj.SetActive(true);

        // Set layer according to level of starting node
        npcObj.layer = LayerMask.NameToLayer(Level2NPCLayer(nodeBlock.level));


        // Randomize NPC attributes
        NPC npc = npcObj.GetComponent<NPC>();
        RandomizeNPC(npc, role);

        // Set NPC Movement variables
        NPCMovement npcMovement = npcObj.GetComponent<NPCMovement>();
        npcMovement.currentNode = nodeBlock.parentNode;
        npcMovement.currentLevel = nodeBlock.level;

        nPeople++;

        // Log NPC in Dialogue Manager
        Managers.Dialogue.LogNPCDialogue(npc);

        //Debug.Log($"{npcObj.name} being placed in {nodeBlock.parentNode.ToString()} at {new Vector3(nodeWorldPosition.x, nodeWorldPosition.y, nodeWorldPosition.z - 1)}, ended up at {npcObj.transform.position}");

    }

    //private void LogNPC()
    //{
    //    Managers.NPC.LogNPC(NPC npc);

    //}

    public void PlaceNPCInBuilding(BuildingTree building)
    {


        //LogNPC(npc);
    }
}
