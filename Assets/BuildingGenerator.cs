using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    private int nLevels;
    [SerializeField] GameObject levelPrefab;
    [SerializeField] float levelHeight;
    [SerializeField] float levelWidth;

    private BoxCollider2D _collider;
    private BoxCollider2D _trigger;

    // Start is called before the first frame update
    void Start()
    {
        nLevels = Random.Range(1, 4);

        for (int i = 0; i < nLevels; i++)
        {
            GameObject levelClone = Instantiate(levelPrefab);
            levelClone.transform.parent = transform;

            Vector3 levelPosition = new Vector3();
            levelPosition.x = 0;
            levelPosition.z = 0;
            levelPosition.y = (i + 1) * levelHeight;
            levelClone.transform.localPosition = levelPosition;

        }

        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();

        _collider = colliders[0];
        _trigger = colliders[1];

        _collider.size = new Vector3(levelWidth, levelHeight * (nLevels + 1));
        _collider.offset = new Vector3(0, nLevels * levelHeight / 2, 0);

        _trigger.size = new Vector3(levelWidth, levelHeight * (nLevels + 1));
        _trigger.offset = new Vector3(0, nLevels * levelHeight / 2, 0);
        _trigger.isTrigger = true;
    }
}
