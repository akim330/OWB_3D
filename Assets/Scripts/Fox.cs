using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Pursuit
}

public class Fox : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController frontHeadController;
    [SerializeField] AnimatorOverrideController backHeadController;
    [SerializeField] AnimatorOverrideController sideHeadController;

    private Animator _anim;

    private MonsterState state;
    private SphereCollider pursuitTrigger;

    private List<Transform> targets;
    private Vector3 currentTarget;

    private Rigidbody _body;

    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float crouchTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackCoolDownTime;
    [SerializeField] private float attackSpeed;

    private bool midAttack;

    private SpriteRenderer _parentRenderer;

    private Camera _cam;

    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _parentRenderer = GetComponent<SpriteRenderer>();
        if (_parentRenderer != null)
        {
            _parentRenderer.enabled = false;
        }

        _cam = Camera.main;
    }

    private void Start()
    {
        state = MonsterState.Idle;

        targets = new List<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        if (midAttack)
        {
            return;
        }

        if (targets.Count > 0)
        {
            SetTarget();

            // If in attack range, attack
            var attackDirection = currentTarget - transform.position;
            if ((currentTarget - transform.position).magnitude <= attackRange) 
            {
                midAttack = true;
                StartCoroutine(AttackCoroutine(attackDirection.normalized));
            }
            else // Out of range => keep moving
            {
                // Face target
                var camDirectionToThis = _cam.transform.InverseTransformDirection(transform.position - _cam.transform.position);
                if (camDirectionToThis.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                // Move
                var movement = (currentTarget - transform.position).normalized * speed * Time.deltaTime;
                Debug.Log($"currentTarget: {currentTarget}\n" +
                    $"fox: {transform.position}\n" +
                    $"diff: {(currentTarget - transform.position).normalized}\n" +
                    $"Movement: {movement}");
                _body.MovePosition(transform.position + movement);
                _anim.SetFloat("speed", speed);
            }

        }
        else
        {
            _anim.SetFloat("speed", 0f);
        }
    }

    private IEnumerator AttackCoroutine(Vector3 attackDirection)
    {
        Debug.Log("0: Starting coroutine");
        // Initial crouch
        _anim.SetTrigger("crouch");
        yield return new WaitForSeconds(crouchTime);
        Debug.Log("1: End crouch");


        // Trigger attack
        _anim.SetTrigger("attack");
        var timer = 0f;
        while (timer <= attackTime)
        {
            timer += Time.deltaTime;
            _body.MovePosition(transform.position + attackDirection * attackSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("1: End attack");


        // End crouch
        _anim.SetTrigger("crouch");
        yield return new WaitForSeconds(attackCoolDownTime);

        Debug.Log("2: end attack");

        // End sequence
        _anim.SetTrigger("endattack");

        midAttack = false;
    }

    private void SetTarget()
    {
        Debug.Log("Setting targets");

        Vector3 closestPosition = Vector3.zero;
        var closestDistance = 9999f;
        foreach (Transform target in targets)
        {
            var currentDistance = Vector3.Distance(target.position, transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestPosition = target.position;
            }
        }
        currentTarget = closestPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Adding to targets");
            targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.Remove(other.transform);
        }
    }
}
