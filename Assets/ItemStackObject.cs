using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStackObject : MonoBehaviour
{
    [SerializeField] private ItemStack _itemStack;
    public ItemStack itemStack
    {
        get { return _itemStack; }
        set
        {
            _itemStack = value;
            UpdateItem();
        }
    }


    public int count;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private PolygonCollider2D _trigger;
    [SerializeField] private PolygonCollider2D _collider;

    [Header("Magnet")]
    private bool flying;
    private Transform flyingTarget;
    [SerializeField] private float speed = 5f;

    private float originalGravity;

    private Rigidbody2D _body;

    private float magnetCoolDown = 5f;
    private float timer;
    private bool inMagnetCoolDown;

    private void OnValidate()
    {
        _renderer = GetComponent<SpriteRenderer>();

        PolygonCollider2D[] colliders = GetComponentsInChildren<PolygonCollider2D>();
        _trigger = colliders[0];
        _collider = colliders[1];
    }

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        originalGravity = _body.gravityScale;

        flying = false;
        UpdateItem();
    }

    private void UpdateItem()
    {
        //Debug.Log($"Updating item: {_item == null}, Renderer: {_renderer == null}");

        if (_itemStack == null)
        {
            _renderer.sprite = null;
            _collider.enabled = false;
            _trigger.enabled = false;
        }
        else
        {
            _collider.enabled = true;
            _trigger.enabled = true;

            _renderer.sprite = _itemStack.item.Icon;
            _collider.points = _itemStack.item.collider.points;
            _trigger.points = _itemStack.item.collider.points;
        }

    }

    public void FlyToTransform(Transform target)
    {
        if (!inMagnetCoolDown)
        {
            flying = true;
            _body.gravityScale = 0;
            _body.velocity = Vector2.zero;
            _body.angularVelocity = 0;
            flyingTarget = target;

        }
        //Debug.Log($"{gameObject.name}: Flying target position: {flyingTarget.position.ToString()}");
    }

    private void Update()
    {
        if (inMagnetCoolDown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                inMagnetCoolDown = false;
            }
        }

        if (flying)
        {
            //Debug.Log($"{gameObject.name}: Flying target position: {flyingTarget.position.ToString()}. Current: {transform.position}. change: {speed * Time.deltaTime * (flyingTarget.position - transform.position)}");


            transform.position = transform.position + speed * Time.deltaTime * (flyingTarget.position - transform.position);
        }
    }

    public void Reset()
    {
        flying = false;
        _body.gravityScale = originalGravity;
        flyingTarget = null;
        itemStack = null;
    }

    public void OnThrow()
    {
        timer = magnetCoolDown;
        inMagnetCoolDown = true;
    }
}
