using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int id;

    public float speed = 10f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        speed = 10f;
    }


    // Update is called once per frame
    void Update()
    {

        // Time.maximumDeltaTime = 0.03f;
        float deltaX = Input.GetAxis("Horizontal") * speed; //* Time.deltaTime;

        Vector2 movement = new Vector2(deltaX, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        _animator.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

    }

}
