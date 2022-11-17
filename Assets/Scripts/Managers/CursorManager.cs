using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class CursorManager : MonoBehaviour, IGameManager 
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private Tilemap tileMap;

    private Camera cam;

    public void Startup()
    {
        cam = Camera.main;
        Debug.Log("Started up Cursor Managers");

    }

    private void Update()
    {
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3Int coordinate = tileMap.WorldToCell(mouseWorldPos);
    }

}
