using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using System.Text;
using System;

public enum TileType
{
    Empty, Solid
}

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private bool _debug;

    [SerializeField] private int worldExtentX;
    [SerializeField] private int worldExtentY;

    [SerializeField] private Tilemap groundTileMap;

    [SerializeField] private BoxCollider groundCollider;

    [SerializeField] private int forestStartX = 0;

    [Header("Dirt")]
    [SerializeField] private Tile[] dirtTiles;

    [Header("Green ground")]
    [SerializeField] private Tile[] greenGroundTiles;

    [Header("Concrete")]
    [SerializeField] private Tile[] concreteTiles;

    [Header("Forest")]
    [SerializeField] private GameObject[] trees;
    [SerializeField] private GameObject[] bushes;
    [SerializeField] private int minSpacing;
    [SerializeField] private GameObject forestParent;

    [Header("Lamps")]
    [SerializeField] private GameObject lamp;

    [Header("NavMesh")]
    [SerializeField] private NavMeshRuntime _mesh;


    //private CompositeCollider2D _collider;

    private int mapWidth;
    private int mapHeight;

    private int startX;
    private int endX;
    private int startY;
    private int endY;

    private StringBuilder sb;

    // Start is called before the first frame update
    public void Startup()
    {
        sb = new StringBuilder();

        mapWidth = 2 * worldExtentX + 1;
        mapHeight = 2 * worldExtentY + 1;

        startX = -1 * worldExtentX;
        endX = worldExtentX;
        startY = -1 * worldExtentY;
        endY = worldExtentY;

        // Regenerate Perlin noise parameters

        GenerateTerrain();

        PlaceTown();

        PlaceForest();

        Debug.Log($"Baking mesh");
        _mesh.Bake();
    }

    public void GenerateTerrain()
    { 
        // Make tile map

        groundTileMap.ClearAllTiles();

        var area = new BoundsInt(startX, startY, 0, mapWidth, mapHeight, 1);
        var tileArray = new TileBase[area.size.x * area.size.y * area.size.z];

        int i = 0;
        for (var y = startY; y < endY; y++) // Absolute units (negative to positive)
        {
            for (var x = startX; x <= endX; x++) // Relative units away from the start (only positive)
            {
                if (x < forestStartX)
                {
                    tileArray[i] = concreteTiles[0];
                }
                else
                {
                    //tileArray[i] = dirtTiles[Random.Range(0, dirtTiles.Length)];
                    tileArray[i] = greenGroundTiles[Random.Range(0, greenGroundTiles.Length)];
                }
                i++;
            }
        }
        groundTileMap.SetTilesBlock(area, tileArray);

        // 


        // Set collider

        groundCollider.center = new Vector3(0.5f * (startX + endX), 0, 0.5f * (startY + endY));
        groundCollider.size = new Vector3(endX - startX, 0, endY - startY);
    }


    private void PlaceTown()
    {
        GameObject townParent;
        Vector3Int size;

        Managers.Town.GenerateTown(out townParent, out size);

        // Where do you want to set the town? For now at zero
        townParent.transform.position = new Vector3(-150f, -1, 0);

        Managers.Town.PlaceNPCsInHouses();
    }

    private void PlaceForest()
    {
        GameObject parent = Instantiate(forestParent, Vector3.zero, Quaternion.identity);

        for (var y = startY; y < endY; y++) // Absolute units (negative to positive)
        {
            for (var x = startX; x <= endX; x++) // Relative units away from the start (only positive)
            {
                if (x >= forestStartX)
                {
                    // Chance of placing anything
                    if (Random.Range(0, 100) == 0)
                    {
                        int choice = Random.Range(0, 4);
                        GameObject instantiated;

                        if (choice == 0)
                        {
                            instantiated = Instantiate(trees[Random.Range(0, trees.Length)], new Vector3(x, -1, y), Quaternion.identity);
                            float randomScale = Random.Range(0.5f, 1f);
                            instantiated.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                        }
                        else
                        {
                            instantiated = Instantiate(bushes[Random.Range(0, bushes.Length)], new Vector3(x, -1, y), Quaternion.identity);
                            float randomScale = Random.Range(0.5f, 1f);
                            instantiated.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                        }
                        instantiated.transform.parent = parent.transform;
                        
                    }
                }
            }
        }
    }

    //private void PlaceTrees(string objectType)
    //{
    //    int placeEvery;

    //    GameObject prefab;
    //    if (objectType == "tree")
    //    {
    //        prefab = tree;
    //        placeEvery = 20;
    //    }
    //    else if (objectType == "lamp")
    //    {
    //        prefab = lamp;
    //        placeEvery = 15;
    //    }
    //    else
    //    {
    //        Debug.LogError("objecType not recognized");
    //        prefab = null;
    //        placeEvery = 20;
    //    }

    //    int coolDown = 0;

    //    for (int x = 0; x < mapWidth; x++)
    //    {
    //        if (objectType == "tree")
    //        {
    //            if (x >= townStartIdx && x <= townStartIdx + townWidth)
    //            {
    //                continue;
    //            }
    //        }
    //        else if (objectType == "lamp")
    //        {
    //            if (x <= townStartIdx || x >= townStartIdx + townWidth)
    //            {
    //                continue;
    //            }
    //        }

    //        // Check if cool down is 0 to ensure minimum spacing
    //        if (coolDown == 0)
    //        {
    //            // Place tree with probability 1/20 only if the ground is level
    //            if (Random.Range(0, placeEvery) == 0 && CheckLevelGround(x))
    //            {

    //                float yPos = _finalHeights[x] + 1;
    //                GameObject treeClone = Instantiate(prefab, new Vector3(x - worldExtentX, yPos, tree.transform.position.z), Quaternion.Euler(Vector3.zero));
    //                treeClone.SetActive(true);
    //                coolDown = minSpacing;
    //            }
    //        }
    //        else if (coolDown > 0)
    //        {
    //            coolDown--;
    //        }
    //        else
    //        {
    //            Debug.LogError($"ERROR: coolDown is negative: {coolDown}");
    //        }
    //    }
    //}

    //private float GetSurfaceNoiseValue(int x)
    //{
    //    return surfaceNoise[x];
    //}

    private void PrintArray(float[] arr)
    {
        Debug.Log("Array = [" + String.Join(",",
            new List<float>(arr)
            .ConvertAll(i => i.ToString())
            .ToArray()) + "]");
    }


    public TileType GetTileType(Vector2Int pos)
    {
        Tile tile = (Tile)groundTileMap.GetTile((Vector3Int)pos);
        if (tile == null)
        {
            return TileType.Empty;
        }
        else
        {
            return TileType.Solid;
        }
    }

    //private float AdjustedSigmoid(float x)
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
