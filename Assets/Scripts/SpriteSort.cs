using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

public enum RendererType
{
    Sprite,
    Tilemap,
    SortingGroup,
    None
}

public class SpriteSort : MonoBehaviour
{
    [SerializeField] private float layer;

    private static Transform _camTransform;
    private SpriteRenderer _renderer;
    private TilemapRenderer _tilemapRenderer;
    private SortingGroup _sortingGroup;

    private RendererType _type;
    private Vector3 displacement;
    private Vector3 projection;

    // Start is called before the first frame update
    void Start()
    {
        _camTransform = Camera.main.transform;
        _sortingGroup = GetComponent<SortingGroup>();

        if (_sortingGroup == null)
        {
            _renderer = GetComponent<SpriteRenderer>();

            if (_renderer == null)
            {
                _tilemapRenderer = GetComponent<TilemapRenderer>();

                if (_tilemapRenderer != null)
                {
                    _type = RendererType.Tilemap;
                }
                else
                {
                    _type = RendererType.None;
                }
            }
            else
            {
                _type = RendererType.Sprite;
            }
        }
        else
        {
            _type = RendererType.SortingGroup;
        }


        UpdateSortingOrder();
    }

    private void Update()
    {
        UpdateSortingOrder();
    }

    void UpdateSortingOrder()
    {
        if (_type == RendererType.None)
        {
            return;
        }

        displacement = transform.position - _camTransform.position;

        // look: forward vector from camera
        Vector3 look = _camTransform.TransformDirection(Vector3.forward);

        // projection: distance along the forward direction 
        projection = Vector3.Project(displacement, look);

        if (_type == RendererType.SortingGroup)
        {
            _sortingGroup.sortingOrder = Mathf.RoundToInt(-1 * Vector3.Magnitude(projection) * layer);
        }
        else if (_type == RendererType.Sprite)
        {
            // 
            _renderer.sortingOrder = Mathf.RoundToInt(-1 * Vector3.Magnitude(projection) * layer);
        }
        else if (_type == RendererType.Tilemap)
        {
            _tilemapRenderer.sortingOrder = Mathf.RoundToInt(-1 * Vector3.Magnitude(projection) * layer);
        }
    }
}
