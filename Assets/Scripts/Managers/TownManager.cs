using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] GameObject[] peoplePrefabs;

    private int _townStartX;
    private int _townEndX;

    private int nPeople;


    public void Startup()
    {
        nPeople = 0;
    }

    public void SetTownCoordinates(int startX, int endX)
    {
        _townStartX = startX;
        _townEndX = endX;
    }

    public void PlacePeople()
    {
        int coolDown = 0;

        for (int x = _townStartX; x < _townEndX; x++)
        {
            // Check if cool down is 0 to ensure minimum spacing
            if (coolDown == 0)
            {
                // Place person with probability 1/5 only if the ground is level
                if (Random.Range(0, 5) == 0)
                {
                    GameObject chosen = peoplePrefabs[Random.Range(0, peoplePrefabs.Length)];
                    GameObject personClone = Instantiate(chosen, new Vector3(x, 10, chosen.transform.position.z), Quaternion.Euler(Vector3.zero));
                    personClone.name = $"Person{nPeople.ToString()}";
                    personClone.SetActive(true);

                    // Set NPC id
                    NPC npc = personClone.GetComponent<NPC>();
                    npc.id = nPeople;

                    coolDown = 0;

                    nPeople++;
                }
            }
            else if (coolDown > 0)
            {
                coolDown--;
            }
            else
            {
                Debug.LogError($"ERROR: coolDown is negative: {coolDown}");
            }
        }
    }
}
