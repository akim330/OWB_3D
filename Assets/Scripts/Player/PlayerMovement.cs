using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 250f;
    public float jumpForce = 12f;

    [Header("StepClimb")]
    [SerializeField] private float stepHeight;
    [SerializeField] private float stepSmooth;
    [SerializeField] private GameObject stepRayLower;
    [SerializeField] private GameObject stepRayUpper;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BoxCollider2D _box;

    private bool grounded = false;

    [Header("Debugging")]
    [SerializeField] private bool infiniteJump;

    private void OnValidate()
    {
        _box = GetComponent<BoxCollider2D>();

        stepRayLower.transform.position = new Vector3(_box.bounds.max.x, _box.bounds.min.y, transform.position.z);
        stepRayUpper.transform.position = new Vector3(_box.bounds.max.x, stepRayLower.transform.position.y + stepHeight, transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();

        stepRayLower.transform.position = new Vector3(_box.bounds.max.x, _box.bounds.min.y, transform.position.z);
        stepRayUpper.transform.position = new Vector3(_box.bounds.max.x, stepRayLower.transform.position.y + stepHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = speed * 2;
        }
        else
        {
            currentSpeed = speed;
        }

        // Time.maximumDeltaTime = 0.03f;
        float deltaX = Input.GetAxis("Horizontal") * currentSpeed; //* Time.deltaTime;

        Vector2 movement = new Vector2(deltaX, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        _animator.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        //Collider2D[] hits = Physics2D.OverlapAreaAll(corner1, corner2);

        //bool grounded = false;

        //foreach (Collider2D collider in hits)
        //{
        //    ContactPoint2D[] contacts = new ContactPoint2D[10];
        //    int nContacts = collider.GetContacts(contacts);
        //    Debug.Log($"nContacts: {nContacts}, contacts length: {contacts.Length}");

        //    foreach (ContactPoint2D contact in contacts)
        //    {
        //        Debug.Log($"Normal: {contact.normal.ToString()}, Dot: {Vector2.Dot(contact.normal, Vector2.up)}");

        //        if (Vector2.Dot(contact.normal, Vector2.up) > 0.8)
        //        {
        //            grounded = true;
        //        }
        //    }
        //}




        //if (hit != null)
        //{
        //    grounded = true;
        //}

        if (grounded)
        {
            _animator.SetBool("jumping", false);
        }
        else
        {
            _animator.SetBool("jumping", true);

        }

        //print(hit);
        //print(grounded);

        if ((infiniteJump || grounded) && Input.GetKeyDown(KeyCode.W))
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (deltaX != 0)
        {
            StepClimb();
        }
        
    }

    private void StepClimb()
    {
        //Debug.Log("Checking step climb");
        if (Physics2D.Raycast(stepRayLower.transform.position, transform.TransformDirection(transform.right) * transform.localScale.x, 0.1f))
        {
            //Debug.Log("Lower contact!");
            if (!Physics2D.Raycast(stepRayUpper.transform.position, transform.TransformDirection(transform.right) * transform.localScale.x, 0.2f))
            {
                //Debug.Log("No upper contact!");

                _rigidbody.position += new Vector2(0, stepSmooth * Time.deltaTime);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.contacts != null)
        {
            //Debug.Log($"Length of contacts ({col.contacts.GetType()}): {col.contacts.Length}");

            bool foundGround = false;

            foreach (ContactPoint2D contact in col.contacts)
            {
                //Debug.Log($"Normal: {contact.normal.ToString()}, Dot: {Vector2.Dot(contact.normal, Vector2.up)}");
                if (Vector2.Dot(contact.normal, Vector2.up) > 0.8)
                {
                    foundGround = true;
                }
            }

            if (foundGround)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

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

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Debug.Log($"{transform.TransformDirection(transform.forward)}");
        Gizmos.DrawRay(stepRayLower.transform.position, transform.TransformDirection(transform.right) * transform.localScale.x * 0.1f);

        //Gizmos.DrawRay(stepRayLower.transform.position, transform.TransformDirection(transform.forward) * 0.1f);

        //Vector3 max = _box.bounds.max;
        //Vector3 min = _box.bounds.min;
        //Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        //Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        //Gizmos.DrawCube(new Vector3((corner1.x + corner2.x) / 2, (corner1.y + corner2.y) / 2, 0),
        //    new Vector3(corner2.x - corner1.x, (corner1.y - corner2.y), 1)
        //    );
    }
}
