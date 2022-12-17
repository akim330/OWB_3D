using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    public float gravity = -9.8f;
    public float minFall = -1.5f;
    public float terminalVelocity = -10.0f;
    public float jumpImpulse = 15.0f;

    private NavMeshAgent _agent;
    private Animator _animator;

    private float _vertSpeed;
    private bool inTransit;

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.speed = speed;
        inTransit = false;
    }

    private void Start()
    {

        _vertSpeed = minFall;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"animator speed: {_animator.speed}");

        bool moving = true;

        if (inTransit && !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            //Debug.Log($"{gameObject.name} couldn't find a commplete path!");
            _agent.isStopped = true;
            _agent.ResetPath();
            inTransit = false;
        }

        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    //if (inTransit)
                    //{
                    //    Debug.Log($"{gameObject.name} has reached destination!");
                    //}
                    inTransit = false;
                    _animator.SetFloat("speed", 0f);
                    moving = false;
                }
            }
        }

        if (moving)
        {
            _animator.SetFloat("speed", speed);
        }

        //if (inTransit)
        //{
        //    Debug.Log($"{gameObject.name}: pathPending: {_agent.pathPending} | destination: {_agent.destination} | transform: {_agent.transform.position} | remainingDistance: {_agent.remainingDistance} | moving: {moving}");

        //}

        // Gravity
        //if (!grounded)
        //{
        //    _vertSpeed += gravity * 5 * Time.deltaTime;
        //    if (_vertSpeed < terminalVelocity)
        //    {
        //        _vertSpeed = terminalVelocity;
        //    }
        //    if (_contact != null)
        //    {
        //        _animator.SetBool("jumping", true);
        //    }
        //}

        //movement.y = _vertSpeed;
        //// Debug.Log(movement.y);

        //movement *= Time.deltaTime;
    }

    public void SetLandmarkDestination(NPCLandmark landmark)
    {
        _agent.SetDestination(landmark.transform.TransformPoint(Vector3.zero));
        inTransit = true;
    }

    void OnDrawGizmosSelected()
    {
        if (_agent == null || _agent.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.startWidth = 0.5f;
            line.endWidth = 0.5f;
            line.startColor = Color.yellow;
            line.endColor = Color.yellow;
        }

        var path = _agent.path;

        line.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }

}
