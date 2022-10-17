using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Random = UnityEngine.Random;
using CustomArrayExtensions;
using System.Text;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private bool _debug;

    [SerializeField] private int worldExtentX;
    [SerializeField] private int worldExtentY;

    [SerializeField] private int skyStartY;

    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap buildingTileMap;
    [SerializeField] private Tilemap interiorTileMap;
 
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
    void Start()
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

        PlaceTrees();

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
                    int randomChoice = Random.Range(1, 4);
                    int currentHeight = _finalHeights[townStartIdx + x] + 1;

                    BuildingTile currentTile;
                    int widthToAdd;

                    if (randomChoice == 1) // Store
                    {
                        currentTile = Managers.Building.GetStoreTile();
                        widthToAdd = currentTile.width;

                        Tile exterior = new Tile();
                        exterior.gameObject = currentTile.exteriorTileObject;
                        Tile interior = new Tile();
                        interior.gameObject = null;


                        if (widthToAdd < townWidth - x)
                        {
                            //Debug.Log($"Instantiating a store at {-1 * worldExtentX + townStartIdx + x},{currentHeight}");


                            buildingTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0), exterior);
                            //buildingTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0), currentTile.exteriorTile);
                            interiorTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0), interior);

                            sb.AppendFormat("\nx = {0}: Random outcome: generated store!", x);
                        }
                        else
                        {
                            sb.AppendFormat("\nx = {0}: Random outcome: too wide: {1}, {2}, {3}!", x, townWidth, x, widthToAdd);

                        }


                    }
                    else if (randomChoice >= 2) // Building
                    {
                        GameObject building;
                        Managers.Building.GetRandomBuilding(1, 4, out building, out widthToAdd);

                        building.transform.position = new Vector3(-1 * worldExtentX + townStartIdx + x + 0.5f, currentHeight + 0.5f, 10);

                        // Graveyard 1

                        //Tile exterior = new Tile();
                        //exterior.gameObject = Instantiate(building);
                        //exterior.gameObject.GetComponent<BuildingParent>().PopulateDoorParents();
                        ////Debug.Log($"Populating properties of: {building.name}");
                        //exterior.gameObject.GetComponentInChildren<InteriorParent>().PopulateProperties();

                        ////Debug.Log($"Number of sprites: {building.GetComponentInChildren<InteriorParent>().sprites.Length}");


                        //buildingTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0), exterior);

                        //Tile debugTile = (Tile)buildingTileMap.GetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0));

                        //GameObject obj = buildingTileMap.GetInstantiatedObject(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0));
                        //obj = building;

                        ////Debug.Log($"Number of sprites: {building.GetComponentInChildren<InteriorParent>().sprites.Length}");

                        //Debug.Log($"Number of sprites in instance {debugTile.gameObject.GetInstanceID()}: {debugTile.gameObject.GetComponentInChildren<InteriorParent>().sprites.Length}");
                        //Debug.Log($"Number of sprites in instance {obj.GetInstanceID()}: {obj.GetComponentInChildren<InteriorParent>().sprites.Length}");

                        //building.SetActive(false);


                        // Graveyard 2

                        //currentTile = Managers.Building.GetBuildingBaseTile();
                        //widthToAdd = currentTile.width;

                        //Tile exterior = new Tile();
                        //exterior.gameObject = currentTile.exteriorTileObject;
                        //Tile interior = new Tile();
                        //interior.gameObject = currentTile.interiorTileObject;


                        //if (widthToAdd < townWidth - x)
                        //{
                        //    int nLevels = Random.Range(1, 4);


                        //    buildingTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0),
                        //        exterior);
                        //    interiorTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0),
                        //        interior);


                        //    for (int j = 0; j < nLevels; j++)
                        //    {
                        //        currentTile = Managers.Building.GetBuildingLevelTile();
                        //        currentHeight += currentTile.height;

                        //        exterior = new Tile();
                        //        exterior.gameObject = currentTile.exteriorTileObject;
                        //        interior = new Tile();
                        //        interior.gameObject = currentTile.interiorTileObject;

                        //        buildingTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0), exterior);
                        //        interiorTileMap.SetTile(new Vector3Int(-1 * worldExtentX + townStartIdx + x, currentHeight, 0), interior);

                        //    }
                        //    sb.AppendFormat("\nx = {0}: Random outcome: generated building with {1} levels", x, nLevels);

                        //}
                        //else
                        //{
                        //    sb.AppendFormat("\nx = {0}: Random outcome: too wide: {1}, {2}, {3}!", x, townWidth, x, widthToAdd);
                        //}

                    }
                    else
                    {
                        Debug.Log("Error! Something wrong happened");
                        widthToAdd = -1;
                    }

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

        Managers.Town.PlacePeople();
    }

    private void PlaceTrees()
    {
        int coolDown = 0;

        for (int x = 0; x < mapWidth; x++)
        {
            // Check if cool down is 0 to ensure minimum spacing
            if (coolDown == 0)
            {
                // Place tree with probability 1/20 only if the ground is level
                if (Random.Range(0, 20) == 0 && CheckLevelGround(x))
                {

                    float yPos = _finalHeights[x] + 1;
                    GameObject treeClone = Instantiate(tree, new Vector3(x - worldExtentX, yPos, tree.transform.position.z), Quaternion.Euler(Vector3.zero));
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
        //Debug.Log($"Mapwidth: {mapWidth} | Initialized Perlin heights: {_perlinHeights.Length}");

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

        GenerateSurfaceNoise();
        //PrintArray(surfaceNoise);

        //Vector3Int[] positions = new Vector3Int[(endX - startX + 1) * (endY - startY + 1)];
        //Tile[] tiles = new Tile[(endX - startX + 1) * (endY - startY + 1)];

        //int i = 0;
        //for (int x = startX; x <= endX; x++)
        //{
        //    for (int y = startY; y <= endY; y++)
        //    {
        //        //positions[i] = new Vector3Int(x, y, 0);
        //        tiles[i] = dirtTile;
        //        i++;
        //        //tileMap.SetTile(new Vector3Int(x, y, 0), dirtTile);
        //    }
        //}

        //tileMap.SetTilesBlock(new BoundsInt(new Vector3Int((endX + startX) / 2, (endY + startY) / 2, 1), new Vector3Int(worldExtentX * 2, worldExtentY * 2, 0)),
        //    tiles
        //    );

        var area = new BoundsInt(startX, startY, 0, mapWidth, mapHeight, 1);
        var tileArray = new TileBase[area.size.x * area.size.y * area.size.z];

        //Vector3Int[] positions = new Vector3Int[area.size.x * area.size.y];

        int i = 0;
        for (var y = startY; y < endY; y++) // Absolute units (negative to positive)
        {
            for (var x = 0; x < mapWidth; x++) // Relative units away from the start (only positive)
            {
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
                        tileArray[i] = topGrassTiles.GetRandom();
                    }
                    else if (leftNeighbor)
                    {
                        tileArray[i] = upperRightGrassTiles.GetRandom();
                    }
                    else if (rightNeighbor)
                    {
                        tileArray[i] = upperLeftGrassTiles.GetRandom();
                    }
                    else
                    {
                        tileArray[i] = apexGrassTiles.GetRandom();
                    }

                }
                else
                {
                    // This tile is in the middle of its column

                    if (leftNeighbor && rightNeighbor)
                    {
                        tileArray[i] = dirtTiles.GetRandom();
                    }
                    else if (leftNeighbor)
                    {
                        tileArray[i] = rightSideGrassTiles.GetRandom();
                    }
                    else if (rightNeighbor)
                    {
                        tileArray[i] = leftSideGrassTiles.GetRandom();
                    }
                    else
                    {
                        tileArray[i] = dirtTiles.GetRandom();
                    }
                    
                }
                i++;
            }
        }
        //Debug.Log($"area: {area}, length: {tileArray}");
        //tileMap.SetTiles(positions, tileArray);
        groundTileMap.SetTilesBlock(area, tileArray);

    }

    private float GetSurfaceNoiseValue(int x)
    {
        return surfaceNoise[x];
    }

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

    //private float AdjustedSigmoid(float x)
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
