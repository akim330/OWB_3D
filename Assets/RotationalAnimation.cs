using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    [SerializeField] Tilt45 tilt45;

    private SpriteRenderer _renderer;

    [SerializeField] Camera _cam;

    private float nSprites;

    private void OnValidate()
    {
        tilt45 = GetComponent<Tilt45>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = sprites[0];
        nSprites = sprites.Length;

        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //int n = Mathf.FloorToInt(tilt45.angle / (360f / (nSprites - 1)));

        Vector3 displacement = _cam.transform.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, new Vector2(displacement.x, displacement.z)) + 180;

        //int n = Mathf.FloorToInt(angle / (360f / (nSprites - 1)));
        int n = Mathf.FloorToInt(_cam.transform.eulerAngles.y / (360f / (nSprites - 1)));
        Debug.Log($"angle: {angle}, nSprites: {nSprites}, n: {n}");
        _renderer.sprite = sprites[n];
    }
}
