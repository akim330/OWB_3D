using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Random = UnityEngine.Random;
using CustomArrayExtensions;
using System.Text;

public enum TileType
{
    Empty,
    Solid
}

public enum TerrainType
{
    Dirt,
    Concrete
}

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private bool _debug;

    [SerializeField] private int worldExtentX;
    [SerializeField] private int worldExtentY;

    [SerializeField] private int skyStartY;

    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap buildingTileMap;
    [SerializeField] private Tilemap interiorTileMap;

    private TerrainType currentTerrainType;
 
    [Header("Dirt")]
    [SerializeField] private Tile[] dirtTiles;
    [SerializeField] private Tile[] topDirtTiles;
    [SerializeField] private Tile[] rightSideDirtTiles;
    [SerializeField] private Tile[] leftSideDirtTiles;
    [SerializeField] private Tile[] upperRightDirtTiles;
    [SerializeField] private Tile[] upperLeftDirtTiles;
    [SerializeField] private Tile[] apexDirtTiles;

    [SerializeField] private Tile[] topGrassTiles;
    [SerializeField] private Tile[] rightSideGrassTiles;
    [SerializeField] private Tile[] leftSideGrassTiles;
    [SerializeField] private Tile[] upperRightGrassTiles;
    [SerializeField] private Tile[] upperLeftGrassTiles;
    [SerializeField] private Tile[] apexGrassTiles;

    [Header("Concrete")]
    [SerializeField] private Tile[] concreteTiles;
    [SerializeField] private Tile[] topConcreteTiles;
    [SerializeField] private Tile[] rightSideConcreteTiles;
    [SerializeField] private Tile[] leftSideConcreteTiles;
    [SerializeField] private Tile[] upperRightConcreteTiles;
    [SerializeField] private Tile[] upperLeftConcreteTiles;
    [SerializeField] private Tile[] apexConcreteTiles;

    [Header("Sinusoidal Surface Noise")]
    public float multiplier;
    public float[] amplitudes;
    public float[] cycleSizes;

    private float[] surfaceNoise;

    [Header("Perlin Surface Noise")]
    [SerializeField] private float scale;
    [SerializeField] private float perlinMultiplier;
    private float offset;
    private float fixedY;

    [SerializeField] private float scale2;
    [SerializeField] private float perlinMultiplier2;
    private float offset2;
    private float fixedY2;

    [SerializeField] private float metaScale;
    //private float[] weights;
    private float metaOffset;
    private float metaFixedY;

    [Header("Trees")]
    [SerializeField] private GameObject treeParent;
    [SerializeField] private GameObject tree;
    [SerializeField] private int minSpacing;

    [Header("Lamps")]
    [SerializeField] private GameObject lamp;

    [Header("Town")]
    [SerializeField] private bool placeTown;
    [SerializeField] private GameObject storePrefab;
    [SerializeField] private int townWidth;
    [SerializeField] private bool randomPlacement;
    private int townStartIdx;


    //private CompositeCollider2D _collider;

    private int mapWidth;
    private int mapHeight;

    //private int[] heights;

    private float[] _perlinHeights;
    private int[] _finalHeights;

    private StringBuilder sb;

    // Start is called before the first frame update
    void Awake()
    {
        sb = new StringBuilder();

        mapWidth = 2 * worldExtentX + 1;
        mapHeight = 2 * worldExtentY + 1;

        // Regenerate Perlin noise parameters

        offset = Random.Range(0, 99999f);
        fixedY = Random.Range(0, 99999f);

        metaOffset = Random.Range(0, 99999f);
        metaFixedY = Random.Range(0, 99999f);

        offset2 = Random.Range(0, 99999f);
        fixedY2 = Random.Range(0, 99999f);

        FillPerlinHeights();

        CalculateFinalHeights();

        GenerateTerrain();

        PlaceTrees("tree");

        PlaceTrees("lamp");

        PlaceTown();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            GenerateTerrain();
        }
    }

    private void PlaceTown()
    {
        bool storePlaced = false;
        bool cafePlaced = false;
        bool martPlaced = false;
        int nCommercialAdded = 0;

        // Log town coordinates with BiomeManager
        Managers.Biome.townStartIdx = townStartIdx - worldExtentX;
        Managers.Biome.townWidth = townWidth;

        sb = new StringBuilder($"Town starts at: {townStartIdx} and extends {townWidth}");

        int coolDown = 0;

        for (int x = 0; x < townWidth; x++)
        {
            // Check if cool down is 0 to ensure minimum spacing
            if (coolDown == 0)
            {
                // Place building with probability 1/5 only if the ground is level
                if (Random.Range(0, 5) == 0)
                {
                    int randomChoice = Random.Range(1, 6);
                    int currentHeight = _finalHeights[townStartIdx + x] + 1;

                    //BuildingTile currentTile;
                    GameObject building;
                    int widthToAdd;

                    if (randomChoice <= 3 - nCommercialAdded) // Store
                    {
                        bool lookingForUnused = true;
                        BuildingType type = BuildingType.Store;

                        while (lookingForUnused)
                        {
                            int randomCommercialType = Random.Range(0, 3);

                            if (randomCommercialType == 0)
                            {
                                if (!storePlaced)
                                {
                                    type = BuildingType.Store;
                                    lookingForUnused = false;
                                    storePlaced = true;
                                    nCommercialAdded++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (randomCommercialType == 1)
                            {
                                if (!cafePlaced)
                                {
                                    type = BuildingType.Cafe;
                                    lookingForUnused = false;
                                    cafePlaced = true;
                                    nCommercialAdded++;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else if (randomCommercialType == 2)
                            {
                                if (!martPlaced)
                                {
                                    type = BuildingType.Mart;
                                    lookingForUnused = false;
                                    cafePlaced = true;
                                    nCommercialAdded++;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                Debug.LogError($"Something is wrong!");
                                type = BuildingType.Store;
                            }
                        }

                        Managers.Building.GetCommercialTile(type, out building, out widthToAdd);
                    }
                    else if (randomChoice >= 4 - nCommercialAdded) // Building
                    {
                        Managers.Building.GetRandomApartment(1, 4, out building, out widthToAdd);
                    }
                    else
                    {
                        Debug.Log("Error! Something wrong happened");
                        widthToAdd = -1;
                        building = null;
                    }

                    building.transform.position = new Vector3(-1 * worldExtentX + townStartIdx + x + 0.5f, currentHeight + 0.5f, 10);
                    BuildingTree buildingTree = building.GetComponent<BuildingTree>();

                    buildingTree.PopulateAllProperties();

                    Managers.Town.LogBuilding(buildingTree);

                    coolDown = Mathf.FloorToInt(widthToAdd * 1.5f);
                }
                else
                {
                    sb.AppendFormat("\nx = {0}: Random outcome: no generation", x);
                }
            }
            else if (coolDown > 0)
            {
                sb.AppendFormat("\nx = {0}: Cooldown {1}", x, coolDown);

                coolDown--;
            }
            else
            {
                if (_debug)
                {
                    Debug.LogError($"ERROR: coolDown is negative: {coolDown}");
                }
            }
        }
        //Debug.Log(sb.ToString());

        Managers.Town.PlaceNPCsInHouses();
        //Managers.Town.PlacePeopleOutside();
    }

    private void PlaceTrees(string objectType)
    {
        int placeEvery;

        GameObject prefab;
        if (objectType == "tree")
        {
            prefab = tree;
            placeEvery = 20;
        }
        else if (objectType == "lamp")
        {
            prefab = lamp;
            placeEvery = 15;
        }
        else
        {
            Debug.LogError("objecType not recognized");
            prefab = null;
            placeEvery = 20;
        }

        int coolDown = 0;

        for (int x = 0; x < mapWidth; x++)
        {
            if (objectType == "tree")
            {
                if (x >= townStartIdx && x <= townStartIdx + townWidth)
                {
                    continue;
                }
            }
            else if (objectType == "lamp")
            {
                if (x <= townStartIdx || x >= townStartIdx + townWidth)
                {
                    continue;
                }
            }

            // Check if cool down is 0 to ensure minimum spacing
            if (coolDown == 0)
            {
                // Place tree with probability 1/20 only if the ground is level
                if (Random.Range(0, placeEvery) == 0 && CheckLevelGround(x))
                {

                    float yPos = _finalHeights[x] + 1;
                    GameObject treeClone = Instantiate(prefab, new Vector3(x - worldExtentX, yPos, tree.transform.position.z), Quaternion.Euler(Vector3.zero));
                    treeClone.SetActive(true);
                    coolDown = minSpacing;
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

    private bool CheckLevelGround(int x)
    {
        if (x == 0 || x == mapWidth)
        {
            return false;
        }

        float center = Mathf.Ceil(CalculatePerlinHeight(x));
        if (
            center != Mathf.Ceil(CalculatePerlinHeight(x + 1)) ||
            center != Mathf.Ceil(CalculatePerlinHeight(x + 2)) ||
            center != Mathf.Ceil(CalculatePerlinHeight(x - 1)) ||
            center != Mathf.Ceil(CalculatePerlinHeight(x - 2))
        )
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void FillPerlinHeights()
    {
        _perlinHeights = new float[mapWidth];

        for (int i = 0; i < mapWidth; i++)
        {
            _perlinHeights[i] = CalculatePerlinHeight(i);
        }
    }

    private void CalculateFinalHeights()
    {
        _finalHeights = new int[_perlinHeights.Length];
        //Debug.Log($"Intializing final heights: {_finalHeights.Length}");

        for (int i = 0; i < _finalHeights.Length; i++)
        {
            _finalHeights[i] = Mathf.CeilToInt(_perlinHeights[i]);
        }

        if (placeTown)
        {

            List<int> levelAreaCandidates = new List<int>();
            int bestDistance = worldExtentX;
            townStartIdx = -1;

            for (int i = 0; i < mapWidth; i++)
            {
                // If we're not too close to the end and if i is the start of a level area
                if ((i < mapWidth - townWidth) && (Mathf.CeilToInt(_perlinHeights[i]) == Mathf.CeilToInt(_perlinHeights[i + townWidth])))
                {
                    if (randomPlacement)
                    {
                        levelAreaCandidates.Add(i);
                    }
                    else
                    {
                        var distanceFromCenter = Mathf.Abs(i - (mapWidth - 1) / 2);
                        if (distanceFromCenter < bestDistance)
                        {
                            bestDistance = distanceFromCenter;
                            townStartIdx = i;
                        }
                    }
                }
            }

            if (randomPlacement)
            {
                townStartIdx = levelAreaCandidates[Random.Range(0, levelAreaCandidates.Count - 1)];
            }

            for (int i = townStartIdx; i < townStartIdx + townWidth; i++)
            {
                _finalHeights[i] = _finalHeights[townStartIdx];
            }

            Managers.Town.SetTownCoordinates(-1 * worldExtentX + townStartIdx, -1 * worldExtentX + townStartIdx + townWidth);
        }
    }

    public void GenerateTerrain()
    {

        //Debug.Log("Regenerating terrain");

        int startX = -1 * worldExtentX;
        int endX = worldExtentX;
        int startY = -1 * worldExtentY;
        int endY = worldExtentY;

        //float[] xs = new float[mapWidth];
        //float[] weights = new float[mapWidth];
        //float[] coords = new float[mapWidth];
        //float[] afterScale = new float[mapWidth];

        //Debug.Log($"mapWidth: {mapWidth}");
        //Debug.Log($"metaScale: {metaScale}");
        //Debug.Log($"metaOffset: {metaOffset}");

        //for (int x = 0; x < mapWidth; x++)
        //{
        //    //weights[x] = Mathf.Pow(Mathf.PerlinNoise(x / mapWidth * metaScale + metaOffset, metaFixedY), 2);
        //    //Debug.Log("printing");
        //    //Debug.Log(Mathf.PerlinNoise(x / mapWidth * metaScale + metaOffset, metaFixedY));
        //    float xCoord = (float) x / (float) mapWidth * metaScale + metaOffset;
        //    xs[x] = x;
        //    afterScale[x] = x / mapWidth * metaScale;
        //    coords[x] = xCoord;
        //    weights[x] = Mathf.PerlinNoise(xCoord, metaFixedY);
        //}
        //PrintArray(xs);
        //PrintArray(afterScale);
        //PrintArray(coords);
        //PrintArray(weights);

        // Make tile map

        groundTileMap.ClearAllTiles();

        // GenerateSurfaceNoise();

        Tile[] topTiles;
        Tile[] upperRightTiles;
        Tile[] upperLeftTiles;
        Tile[] apexTiles;
        Tile[] rightSideTiles;
        Tile[] leftSideTiles;
        Tile[] middleTiles;


        var area = new BoundsInt(startX, startY, 0, mapWidth, mapHeight, 1);
        var tileArray = new TileBase[area.size.x * area.size.y * area.size.z];

        //Vector3Int[] positions = new Vector3Int[area.size.x * area.size.y];

        int i = 0;
        for (var y = startY; y < endY; y++) // Absolute units (negative to positive)
        {
            for (var x = 0; x < mapWidth; x++) // Relative units away from the start (only positive)
            {

                if (x >= townStartIdx && x <= townStartIdx + townWidth)
                {
                    currentTerrainType = TerrainType.Concrete;
                }
                else
                {
                    currentTerrainType = TerrainType.Dirt;
                }


                if (currentTerrainType == TerrainType.Dirt)
                {
                    middleTiles = dirtTiles;
                    topTiles = topGrassTiles;
                    upperRightTiles = upperRightGrassTiles;
                    upperLeftTiles = upperLeftGrassTiles;
                    apexTiles = apexGrassTiles;

                    leftSideTiles = leftSideGrassTiles;
                    rightSideTiles = rightSideGrassTiles;
                }
                else if (currentTerrainType == TerrainType.Concrete)
                {
                    middleTiles = concreteTiles;
                    topTiles = topConcreteTiles;
                    upperRightTiles = upperRightConcreteTiles;
                    upperLeftTiles = upperLeftConcreteTiles;
                    apexTiles = apexConcreteTiles;

                    leftSideTiles = leftSideConcreteTiles;
                    rightSideTiles = rightSideConcreteTiles;
                }
                else
                {
                    middleTiles = dirtTiles;
                    topTiles = topDirtTiles;
                    upperRightTiles = upperRightDirtTiles;
                    upperLeftTiles = upperLeftDirtTiles;
                    apexTiles = apexDirtTiles;

                    leftSideTiles = leftSideDirtTiles;
                    rightSideTiles = rightSideDirtTiles;
                    Debug.LogError("Unrecognized terrain type");
                }

                //float yThreshold = GetSurfaceNoiseValue(x);

                // Get neighboring
                bool rightNeighbor;
                bool leftNeighbor;

                // Left neighbor
                if (x == 0)
                {
                    leftNeighbor = false;
                }
                else
                {
                    leftNeighbor = y <= _finalHeights[x - 1];
                }

                // Right neighbor
                if (x == mapWidth - 1)
                {
                    rightNeighbor = false;
                }
                else
                {
                    //Debug.Log($"x: {x}, _finalHeights: {_finalHeights.Length}");
                    rightNeighbor = y <= _finalHeights[x + 1];

                }

                //positions[i] = new Vector3Int(x, y, 0);

                if (y > _finalHeights[x])
                {
                    //Debug.Log("Placing empty");
                    tileArray[i] = null;
                }
                else if (y == _finalHeights[x])
                {
                    // This tile is at the top of its column

                    if (leftNeighbor && rightNeighbor)
                    {
                        tileArray[i] = topTiles.GetRandom();
                    }
                    else if (leftNeighbor)
                    {
                        tileArray[i] = upperRightTiles.GetRandom();
                    }
                    else if (rightNeighbor)
                    {
                        tileArray[i] = upperLeftTiles.GetRandom();
                    }
                    else
                    {
                        tileArray[i] = apexTiles.GetRandom();
                    }

                }
                else
                {
                    // This tile is in the middle of its column

                    if (leftNeighbor && rightNeighbor)
                    {
                        tileArray[i] = middleTiles.GetRandom();
                    }
                    else if (leftNeighbor)
                    {
                        tileArray[i] = rightSideTiles.GetRandom();
                    }
                    else if (rightNeighbor)
                    {
                        tileArray[i] = leftSideTiles.GetRandom();
                    }
                    else
                    {
                        tileArray[i] = middleTiles.GetRandom();
                    }
                    
                }
                i++;
            }
        }
        //Debug.Log($"area: {area}, length: {tileArray}");
        //tileMap.SetTiles(positions, tileArray);
        groundTileMap.SetTilesBlock(area, tileArray);

    }

    //private float GetSurfaceNoiseValue(int x)
    //{
    //    return surfaceNoise[x];
    //}

    private void GenerateSurfaceNoise()
    {
        surfaceNoise = WeightedSum(amplitudes, cycleSizes);

        // Subtract mean
        float meanValue = 0;
        for (int i = 0; i < surfaceNoise.Length; i++)
        {
            meanValue += surfaceNoise[i];
        }
        meanValue = meanValue / surfaceNoise.Length;

        for (int i = 0; i < surfaceNoise.Length; i++)
        {
            surfaceNoise[i] = (surfaceNoise[i] - meanValue) * multiplier;
        }

        //Debug.Log($"Max surface noise: {Mathf.Max(surfaceNoise)} | Min surface noise: {Mathf.Min(surfaceNoise)}");
        //Debug.Log("Final");
        //PrintArray(surfaceNoise);
    }

    private void PrintArray(float[] arr)
    {
        Debug.Log("Array = [" + String.Join(",",
            new List<float>(arr)
            .ConvertAll(i => i.ToString())
            .ToArray()) + "]");
    }

    private float[] WeightedSum(float[] amplitudes, float[] cycleSizes)
    {
        if (amplitudes.Length != cycleSizes.Length)
        {
            Debug.LogError("Error: amplitudes and cycleSizes not the same length!");
            return null;
        }

        float[][] baseCurves = new float[cycleSizes.Length][];

        for (int i = 0; i < cycleSizes.Length; i++)
        {
            baseCurves[i] = BaseCurve(cycleSizes[i]);
        }

        float[] totalValues = new float[mapWidth];
        for (int x = 0; x < mapWidth; x++)
        {
            float sum = 0;
            for (int i = 0; i < cycleSizes.Length; i++)
            {
                sum += amplitudes[i] * baseCurves[i][x]; 
            }
            totalValues[x] = sum;
        }

        //Debug.Log($"Weighted sum");
        //PrintArray(totalValues);

        return totalValues;
    }

    private float[] BaseCurve(float cycleSize)
    {
        float phase = Random.Range(0, 2 * Mathf.PI);
        float[] values = new float[mapWidth];
        for (int x = 0; x < mapWidth; x++)
        {
            values[x] = Mathf.Sin(2 * Mathf.PI / cycleSize * x + phase);
        }

        //Debug.Log($"cycleSize: {cycleSize}");
        //PrintArray(values);
        return values;
    }

    private float CalculatePerlinHeight(float x)
    {
        float weight = Mathf.PerlinNoise((float) x / mapWidth * metaScale + metaOffset, metaFixedY);
        weight = Mathf.Clamp(weight, 0, 1);

        weight = 1 / (1 + Mathf.Exp(-12 * weight + 6));
        float noise1 = perlinMultiplier * (Mathf.PerlinNoise((float) x / mapWidth * scale + offset, fixedY) - 0.5f);
        float noise2 = perlinMultiplier2 * (Mathf.PerlinNoise((float) x / mapWidth * scale2 + offset2, fixedY2) - 0.5f);
        return weight * noise1 + (1 - weight) * noise2;
    }

    public TileType GetTileType(Vector2Int pos)
    {
        Tile tile = (Tile) groundTileMap.GetTile((Vector3Int) pos);
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
