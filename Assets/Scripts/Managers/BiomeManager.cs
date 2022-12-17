using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biome
{
    City,
    Forest
}

public class BiomeManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private Transform playerTransform;

    public float townStartIdx;
    public float townWidth;

    private Biome currentBiome;

    public void Startup()
    {
        DetermineBiome();

    }

    public void Start()
    {
        Actions.OnBiomeChanged(currentBiome);
    }

    // Update is called once per frame
    void Update()
    {
        Biome previousBiome = currentBiome;

        DetermineBiome();

        if (currentBiome != previousBiome)
        {
            Actions.OnBiomeChanged(currentBiome);
        }
    }

    private void DetermineBiome()
    {


        if (playerTransform.position.x < townStartIdx)
        {
            currentBiome = Biome.Forest;
        }
        else if (playerTransform.position.x >= townStartIdx && playerTransform.position.x <= townStartIdx + townWidth)
        {
            currentBiome = Biome.City;
        }
        else if (playerTransform.position.x > townStartIdx + townWidth)
        {
            currentBiome = Biome.Forest;
        }
        else
        {
            Debug.LogError("Something went wrong");
        }
    }
}
