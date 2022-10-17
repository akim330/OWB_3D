using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : OperableDevice
{
    private bool _open;

    private Animator _animator;
    private BoxCollider2D _collider;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>(); 
        _open = false;

    }

    public override void Operate()
    {
        //Debug.Log("Operating door");

        _open = !_open;
        _animator.SetBool("open", _open);

        if (_open)
        {
            _collider.isTrigger = true;
        }
        else
        {
            _collider.isTrigger = false;
        }
    }
}
