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

    public void PlaceNPCInBuilding(NPCRole role, Building building)
    {
        Vector3 buildingWorldPosition = building.transform.TransformPoint(new Vector3(0, 0, 0));

        GameObject npcObj = Instantiate(basePersonPrefab, buildingWorldPosition, Quaternion.Euler(Vector3.zero));
        //npcObj.transform.position = new Vector3(npcObj.transform.position.x, npcObj.transform.position.y, npcObj.transform.position.z - 10);
        npcObj.name = $"Person{nPeople.ToString()}";
        npcObj.SetActive(true);

        // Randomize NPC attributes
        NPC npc = npcObj.GetComponent<NPC>();
        RandomizeNPC(npc, role);

        nPeople++;

        Managers.Dialogue.LogNPCDialogue(npc);
    }

}
