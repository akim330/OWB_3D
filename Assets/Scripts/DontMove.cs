using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMove : MonoBehaviour
{
    [SerializeField] string ignoreTag;

    private Rigidbody2D _body;
    private Vector3 position, velocity;
    private float angularVelocity;
    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isColliding)
        {
            position = _body.position;
            velocity = _body.velocity;
            angularVelocity = _body.angularVelocity;
        }
    }

    private void LateUpdate()
    {
        if (isColliding)
        {
            _body.position = position;
            _body.velocity = velocity;
            _body.angularVelocity = angularVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Entering collision with {collision.collider.name}"); 
        if (ignoreTag != "" && collision.collider.tag == ignoreTag)
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log($"Exiting collision with {collision.collider.name}");

        if (collision.collider.tag == ignoreTag)
        {
            isColliding = false;
        }
    }
}
