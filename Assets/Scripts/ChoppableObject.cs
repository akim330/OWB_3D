using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreeState
{
    Upright,
    Falling,
    Fallen
}

public class ChoppableObject : MonoBehaviour
{
    [SerializeField] private int health;

    [SerializeField] private float timeBeforeVanish;

    [SerializeField] private Item log;
    [SerializeField] private int nLogs;

    [SerializeField] private ItemStackPool _pool;

    private Animator _animator;

    private TreeState state;

    private BoxCollider2D _collider;

    private void OnValidate()
    {
        _pool = FindObjectOfType<ItemStackPool>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        state = TreeState.Upright;
    }

    private void Update()
    {

        if (state == TreeState.Upright)
        {
            AnimatorClipInfo[] clipInfo;

            clipInfo = _animator.GetCurrentAnimatorClipInfo(0);

            if (clipInfo != null && clipInfo.Length != 0 && clipInfo[0].clip.name == "tree_fall")
            {
                //Debug.Log("Tree is falling");
                state = TreeState.Falling;
            }
        }
        else if (state == TreeState.Falling)
        {
            AnimatorClipInfo[] clipInfo;

            clipInfo = _animator.GetCurrentAnimatorClipInfo(0);

            //Debug.Log($"clipInfo: {clipInfo != null}, {clipInfo.Length}, {clipInfo[0].clip.name}");

            if (clipInfo != null && clipInfo.Length != 0 && clipInfo[0].clip.name == "tree_fallen")
            {
                //Debug.Log("Tree has fallen. Release logs!");

                state = TreeState.Fallen;
                _animator.speed = 0;
                ReleaseLogs();
            }
        }
        else if (state == TreeState.Fallen)
        {
            timeBeforeVanish -= Time.deltaTime;

            if (timeBeforeVanish <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void OnHit(int damage)
    {
        if (state != TreeState.Upright)
        {
            return;
        }

        health -= damage;
        //Debug.Log($"Hit for damage {damage}! {health}");


        if (health <= 0)
        {
            Fall();
        }
        else
        {
            _animator.SetTrigger("hit");
        }
    }

    public void Fall()
    {
        _animator.SetTrigger("fall");
    }

    private void ReleaseLogs()
    {
        for (int i = 0; i < nLogs; i++)
        {


            Vector2 position = new Vector2(_collider.bounds.min.x + _collider.size.y / nLogs * i, _collider.bounds.max.y);
            //Debug.Log($"Log position: {position}");
            _pool.DropItem(log, 1, position, DropStyle.Loot, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == TreeState.Falling && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            state = TreeState.Fallen;
            _animator.speed = 0;
            ReleaseLogs();
        }
    }
}
