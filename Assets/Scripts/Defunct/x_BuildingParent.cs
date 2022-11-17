using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x_BuildingParent : MonoBehaviour
{
    //private BuildingDoor[] doors;
    //public BuildingType type;
    //public int nFloors;

    //private ExteriorParent _exteriorParent;
    //private InteriorParent[] _interiorParents;

    //private LevelParent _activeLevelParent;
    //private int _activeLevel;

    //[SerializeField] private GameObject stairBase;
    //[SerializeField] private GameObject stairLevel;

    //private void Awake()
    //{
    //    _exteriorParent = GetComponentInChildren<ExteriorParent>();

    //    _interiorParents = GetComponentsInChildren<InteriorParent>();

    //    _activeLevelParent = _exteriorParent;
    //    _activeLevel = 0;
    //}

    //public LevelParent LevelToLevelParent(int level)
    //{
    //    if (level == 0)
    //    {
    //        return _exteriorParent;
    //    }
    //    else
    //    {
    //        return _interiorParents[level - 1];
    //    }
    //}

    //public void TransitionToLevel(int level)
    //{
    //    // Deactivate current level
    //    _activeLevelParent.Deactivate();

    //    // Set new level
    //    _activeLevel = level;
    //    _activeLevelParent = LevelToLevelParent(level);
    //    _activeLevelParent.Activate();
    //}

    //public Sprite[] GetActiveSprites()
    //{
    //    return _activeLevelParent.sprites;
    //}

    //public Vector3[] GetActivePositions()
    //{
    //    return _activeLevelParent.positions;
    //}

    //public void CreateBuilding(BuildingTile tile)
    //{
    //    GameObject exteriorChild;
    //    //GameObject middleChild;
    //    GameObject interiorChild;

    //    exteriorChild = Instantiate(tile.exteriorTileObject, _exteriorParent.transform, false);
    //    exteriorChild.transform.localPosition = new Vector3(0, 0, 0);

    //    //middleChild = Instantiate(stairPrefab, _interiorParents[0].transform, false);
    //    //middleChild.transform.localPosition = new Vector3(0, 0, 0);

    //    interiorChild = Instantiate(tile.interiorTileObject, _interiorParents[0].transform, false);
    //    interiorChild.transform.localPosition = new Vector3(0, 0, 0);

    //    PopulateAllProperties();

    //}

    //public void CreateApartmentBuilding(BuildingTile baseTile, BuildingTile levelTile)
    //{

    //    GameObject exteriorChild;
    //    GameObject middleChild;
    //    GameObject interiorChild;

    //    int currentHeight = 0;

    //    for (var i = 0; i < nFloors; i++)
    //    {
    //        BuildingTile currentTile;
    //        GameObject stairPrefab;

    //        if (i == 0)
    //        {
    //            currentTile = baseTile;
    //            stairPrefab = stairBase;
    //        }
    //        else
    //        {
    //            currentTile = levelTile;
    //            stairPrefab = stairLevel;
    //        }

    //        exteriorChild = Instantiate(currentTile.exteriorTileObject, _exteriorParent.transform, false);
    //        exteriorChild.transform.localPosition = new Vector3(0, currentHeight, 0);

    //        middleChild = Instantiate(stairPrefab, _interiorParents[0].transform, false);
    //        middleChild.transform.localPosition = new Vector3(0, currentHeight, 0);

    //        interiorChild = Instantiate(currentTile.interiorTileObject, _interiorParents[1].transform, false);
    //        interiorChild.transform.localPosition = new Vector3(0, currentHeight, 0);

    //        currentHeight += currentTile.height;
    //    }

    //    PopulateAllProperties();

    //    //doors = new BuildingDoor[_exteriorParent.doors.Length + _interiorParents[0].doors.Length + _interiorParents[1].doors.Length];

    //    //_exteriorParent.doors.CopyTo(doors, 0);
    //    //_interiorParents[0].doors.CopyTo(doors, _exteriorParent.doors.Length);
    //    //_interiorParents[1].doors.CopyTo(doors, _exteriorParent.doors.Length + _interiorParents[0].doors.Length);

    //}

    //private void PopulateAllProperties()
    //{
    //    _exteriorParent.PopulateProperties();
    //    foreach (InteriorParent interiorParent in _interiorParents)
    //    {
    //        interiorParent.PopulateProperties();
    //    }
    //}

    ////public void PopulateDoorParents()
    ////{
    ////    doors = GetComponentsInChildren<BuildingDoor>();
    ////    //Debug.Log($"Number of doors: {doors.Length}");

    ////    foreach (BuildingDoor door in doors)
    ////    {
    ////        door.buildingParent = this;
    ////    }
    ////}

    //public Vector2 GetGroundDoorLocation()
    //{
    //    return _exteriorParent.GetGroundDoorLocation();
    //}
}
