using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    public float gravity = -9.8f;
    public float minFall = -1.5f;
    public float terminalVelocity = -10.0f;
    public float jumpImpulse = 15.0f;

    public float pushForce = 3.0f;

    private float _vertSpeed;

    private bool _grounded;
    public bool grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            if (debug)
            {
                if (value)
                {
                    Debug.Log("Grounded!");
                }
                else
                {
                    Debug.Log("Ungrounded!");
                }
            }

            _grounded = value;
        }
    }

    [SerializeField] private float jumpCoolDown;
    private float jumpCoolDownTimer;
    private bool justJumped;

    private CharacterController _charController;
    private Animator _animator;

    private ControllerColliderHit _contact;

    private bool debug;

    private void Start()
    {
        debug = false;

        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _vertSpeed = minFall;

        justJumped = false;

        transform.position = new Vector3(-132f, 1f, -30f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        // (1) Set speed based on keyboard inputs
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        _animator.SetFloat("speed", movement.sqrMagnitude);

        // Old grounded check
        //RaycastHit hit;
        //bool _grounded = false;

        //if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        //{

        //    float check = (_charController.height/2f + _charController.radius) * 1.2f;
        //    _grounded = hit.distance <= check;

        //    Debug.Log($"Hit: {hit.point}, my position: {transform.position}, distance: {hit.distance}, check: {(_charController.height/2f + _charController.radius) * 1.1f}");


        //    //Debug.Log($"Found hit? {hit.collider.name} at distance {hit.distance} | check: {check} | grounded: {_grounded}");
        //}

        // Vertical movement

        // (2) Grounded check

        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpImpulse;
                jumpCoolDownTimer = jumpCoolDown;
                justJumped = true;
            }
            else
            {
                if (justJumped)
                {
                    jumpCoolDownTimer -= Time.deltaTime;
                    if (jumpCoolDownTimer <= 0)
                    {
                        justJumped = false;
                    }
                }
                else
                {
                    _vertSpeed = minFall;
                    _animator.SetBool("jumping", false);
                }
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
            if (_contact != null)
            {
                _animator.SetBool("jumping", true);
            }

            if (_charController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;
        // Debug.Log(movement.y);

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null && !rigidbody.isKinematic)
        {
            rigidbody.velocity = hit.moveDirection * pushForce;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.contacts != null)
        {
            if (debug)
            {
                Debug.Log($"Length of contacts ({col.contacts.GetType()}): {col.contacts.Length}");
            }

            bool foundGround = false;

            foreach (ContactPoint contact in col.contacts)
            {
                if (debug)
                {
                    Debug.Log($"{contact.otherCollider.gameObject.name}: Normal: {contact.normal.ToString()}, Dot: {Vector2.Dot(contact.normal, Vector2.up)}");
                }

                if (Vector2.Dot(contact.normal, Vector2.up) > 0.8)
                {
                    foundGround = true;
                }
            }

            if (foundGround)
            {
                grounded = true;
            }
            //else
            //{
            //    grounded = false;
            //}

            //ContactPoint2D[] contacts = new ContactPoint2D[] { };
            //int nContacts = col.GetContacts(contacts);
            //Debug.Log($"nContacts ({contacts.GetType()}): {nContacts}");
            //foreach (ContactPoint2D contact in contacts)
            //{
            //    if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            //    {
            //        grounded = true;
            //    }

        }
        else
        {
            //Debug.Log("Contacts are null");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (debug)
        {
            Debug.Log($"{collision.gameObject.name} exiting!");
        }
        grounded = false;
    }


}
