
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class x_BuildingTile: ScriptableObject
{
    public Tile exteriorTile;
    public Tile interiorTile;

    public GameObject exteriorTileObject;
    public GameObject interiorTileObject;
    public int width;
    public int height;
}
