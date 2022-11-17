using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapDebug : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log($"Position: {pos.ToString()}");
            Tile tile = (Tile)tilemap.GetTile(Vector3Int.FloorToInt(pos));
            if (tile == null)
            {
                Debug.Log($"Empty tile");
            }
            else
            {
                Debug.Log($"{tile.sprite}");
            }
        }
    }
}
