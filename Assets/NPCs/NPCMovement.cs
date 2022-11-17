using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum NPCBehaviorPattern
{
    Random,
    Scheduled
}

public enum NPCState
{
    Walk,
    Stand
}

public enum NPCCommand
{
    Walk,
    Jump
}

public class NPCMovement : MonoBehaviour
{
    public NPCBehaviorPattern currentPattern;

    //public NPCBehaviorPattern state;
    //[SerializeField] private float minWalk;
    //[SerializeField] private float walkingSpeed;
    private float _walked;

    [SerializeField] private float reachedXBuffer = 0.2f;
    [SerializeField] private float reachedYBuffer = 3f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 35f;
    private float jumpCoolDown = 1f;
    private float jumpCoolDownTimer;
    private bool canJump;

    //private float restartCoolDown = 3f;
    //private float restartCoolDownTimer;

    //private float destinationX;
    private Queue<Vector3> destinations;
    private Vector3 currentDestination;
    private bool enroute;

    private bool inConvo;
    private Transform target;

    [SerializeField] private TerrainGeneration _terrain;

    private bool grounded;

    private Animator _anim;
    private Rigidbody2D _body;
    [SerializeField] private Foot _foot;

    // Z-axis
    public int currentLevel;
    public BuildingNode currentNode;
    NPCLandmark currentLandmark;

    // Platform
    NPCOneWayPlatform platform;

    private bool debug;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;

        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _terrain = FindObjectOfType<TerrainGeneration>();

        //restartCoolDownTimer = 0f;

        inConvo = false;

        if (currentPattern == NPCBehaviorPattern.Random)
        {
            SetRandomDestination();        
        }

        // TODO: CHANGE WHEN THE NPC start point isn't outside
        //currentLevel = 0;
        //gameObject.layer = LayerMask.NameToLayer("NPC");
        //UpdateZ();

        platform = GetComponent<NPCOneWayPlatform>();
    }

    private void UpdateZ()
    {
        transform.position = new Vector3(
            transform.position.x, transform.position.y, 10 * (currentLevel + 1) - 1
            );
    }

    private void OnValidate()
    {
        _foot = GetComponentInChildren<Foot>();
    }

    public void SetRandomDestination()
    {
        destinations = new Queue<Vector3>();
        destinations.Enqueue(Managers.Town.GetRandomBuildingLocation());

        enroute = true;
        canJump = true;
    }

    //public void SetBuildingNodeDestination(BuildingNode node)
    //{
    //    destinationX = node.transform.position.x;
    //    enroute = true;
    //    canJump = true;
    //}

    public void SetLandmarkDestination(NPCLandmark landmark)
    {
        currentLandmark = landmark;
        destinations = new Queue<Vector3>(landmark.GetRoute(currentNode));

        string currentNodeLocation;
        if (currentNode == null)
        {
            currentNodeLocation = "outside";
        }
        else
        {
            currentNodeLocation = $"{currentNode.gameObject.name} in {currentNode.GetBuildingName()}";
        }

        if (debug)
        {
            Debug.Log($"{gameObject.name} going from {currentNodeLocation} to {landmark.ToString()}: \n" +
                $"Route: {Helper.ArrayToString(destinations.ToArray())}"
                );
        }

        currentDestination = destinations.Dequeue();
        enroute = true;
        canJump = true;
    }

    private void UpdateCurrentNode()
    {
        if (currentLevel == 0)
        {
            currentNode = null;
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
            foreach (Collider2D collider in colliders)
            {
                //Debug.Log($"Collider: {collider.gameObject.name}");
                BuildingNodeBlock nodeBlock = collider.GetComponent<BuildingNodeBlock>();
                if (nodeBlock != null && nodeBlock.level == currentLevel)
                {
                    if (debug)
                    {
                        Debug.Log($"{gameObject.name} overlapping with node {nodeBlock.ToString()}");
                    }
                    currentNode = nodeBlock.parentNode;
                }
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        //if (state == NPCBehaviorPattern.Random)
        //{
        //    if (_walked >= minWalk)
        //    {

        //    }
        //}

        if (jumpCoolDownTimer > 0)
        {
            jumpCoolDownTimer -= Time.deltaTime;
            if (jumpCoolDownTimer < 0)
            {
                canJump = true;
            }
        }
        //restartCoolDownTimer -= Time.deltaTime;

        if (inConvo)
        {
            transform.localScale = new Vector3(Mathf.Sign(target.position.x - transform.position.x), 1, 1); 
            _anim.SetFloat("speed", 0);
            return;
        }

        if (enroute)
        {
            if (Mathf.Abs(_foot.transform.position.x - currentDestination.x) < reachedXBuffer) // Reached X
            {
                //Debug.Log($"{gameObject.name}: Frozen x | foot position: {_foot.transform.position.x}, currentDestination: {currentDestination.x}");

                _body.velocity = new Vector2(0, _body.velocity.y);

                if (Mathf.Abs(_foot.transform.position.y - currentDestination.y) < reachedYBuffer) // If also reached Y
                {
                    if (destinations.Count == 0) // No more destinations, we've reached the final destination!
                    {
                        enroute = false;

                        if (debug)
                        {
                            Debug.Log($"Setting {gameObject.name}'s currentNode as {currentLandmark.node} in {currentLandmark.node.GetBuildingName()}");
                        }

                        currentNode = currentLandmark.node;
                        currentLandmark = null;
                    }
                    else // More destinations remain, so continue
                    {
                        Vector3 previousDestination = currentDestination;
                        currentDestination = destinations.Dequeue();

                        if ((previousDestination.x == currentDestination.x) && (previousDestination.y == currentDestination.y) && (previousDestination.z != currentDestination.z))
                        {
                            // Change layers
                            currentLevel = Mathf.FloorToInt((currentDestination.z - 1) / 10);

                            if (debug)
                            {
                                Debug.Log($"{gameObject.name} changing layer to Level{currentLevel}NPC since z = {currentDestination.z}");
                            }

                            if (currentLevel == 0)
                            {
                                gameObject.layer = LayerMask.NameToLayer($"NPC");

                            }
                            else
                            {
                                gameObject.layer = LayerMask.NameToLayer($"Level{currentLevel}NPC");
                            }

                            transform.position = new Vector3(currentDestination.x, currentDestination.y, currentDestination.z - 1);

                            if (debug)
                            {
                                Debug.Log($"Teleporting {gameObject.name} to {currentDestination}");
                            }
                            UpdateCurrentNode();

                            currentDestination = destinations.Dequeue();
                        }
                    }
                }
                else if (currentDestination.y > _foot.transform.position.y) // If not reached Y and Y is higher, then jump!
                {
                    if (canJump && grounded)
                    {
                        //Debug.Log($"{gameObject.name}: jumping to get to {currentDestination.ToString()} and then {Helper.ArrayToString(destinations.ToArray())}");
                        _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        jumpCoolDownTimer = jumpCoolDown;
                        canJump = false;
                    }

                }
                else // destination Y is lower. This is not programmed yet
                {
                    platform.Drop();
                    //Debug.LogError("Destination is lower!");

                }

                //enroute = false;
                //restartCoolDownTimer = restartCoolDown;
            }
            else
            {


                float direction = Mathf.Sign(currentDestination.x - transform.position.x);

                transform.localScale = new Vector3(direction, 1, 1);


                Vector2 movement = new Vector2(speed * direction, _body.velocity.y);
                _anim.SetFloat("speed", speed);

                _body.velocity = movement;

               // Debug.Log($"grounded: {grounded}");

                if (canJump && grounded)
                {
                    NPCCommand command = GetCommandAt(Vector2Int.FloorToInt(new Vector2(_foot.transform.position.x + direction, _foot.transform.position.y)));
                    //Debug.Log($"command: {command}");


                    if (command == NPCCommand.Jump)
                    {
                        //Debug.Log("JUMPING!");
                        _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        jumpCoolDownTimer = jumpCoolDown;
                        canJump = false;
                    }
                }
            }
        }
        else
        {
            _anim.SetFloat("speed", 0);
            if (currentPattern == NPCBehaviorPattern.Random) //&& restartCoolDownTimer < 0)
            {
                SetRandomDestination();
            }
        }
    }


    private NPCCommand GetCommandAt(Vector2Int pos)
    {
        if (_terrain.GetTileType(pos) == TileType.Solid || _terrain.GetTileType(new Vector2Int(pos.x, pos.y + 1)) == TileType.Solid)
        {
            //Debug.Log("Front or upper solid");
            return NPCCommand.Jump;
        }
        else if (_terrain.GetTileType(new Vector2Int(pos.x, pos.y - 1)) == TileType.Empty)
        {
            if (_terrain.GetTileType(new Vector2Int(pos.x, pos.y - 2)) == TileType.Solid || _terrain.GetTileType(new Vector2Int(pos.x, pos.y - 3)) == TileType.Solid)
            {
                return NPCCommand.Walk;
            }
            else
            {
                return NPCCommand.Jump;

            }

        }
        else
        {
            //Debug.Log("Lower solid");

            return NPCCommand.Walk;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("Colliding!");

        if (col.contacts != null)
        {

            //Debug.Log($"Length of contacts ({col.contacts.GetType()}): {col.contacts.Length}");

            bool foundGround = false;

            foreach (ContactPoint2D contact in col.contacts)
            {
                //Debug.Log($"Normal: {contact.normal.ToString()}, Dot: {Vector2.Dot(contact.normal, Vector2.up)}");
                if (Vector2.Dot(contact.normal, Vector2.up) > 0.8)
                {
                    //Debug.Log($"Setting foundGround to true");
                    foundGround = true;
                    break;
                }
            }

            if (foundGround)
            {
                //Debug.Log($"Setting grounded to true");

                grounded = true;
            }
            else
            {
                grounded = false;
            }

        }
        else
        {
           // Debug.Log("Contacts are null");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        grounded = false;
    }

    public void OnStartConversation(Transform initiator)
    {
        target = initiator;
        inConvo = true;
    }

    public void OnEndConversation()
    {
        inConvo = false;
    }

    public void RegisterCollision(NPCLandmark landmark)
    {
        if ((enroute) && (landmark == currentLandmark))
        {
            //Debug.Log($"{gameObject.name} collided with the desired landmark: {landmark.name}!");
            enroute = false;
            //Debug.Log($"Setting {gameObject.name}'s currentNode as {currentLandmark.node} in {currentLandmark.node.GetBuildingName()}");
            currentNode = currentLandmark.node;
            currentLandmark = null;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log($"COLLISION {gameObject.name}: {collision.gameObject.name}");
    //}

    //IEnumerator TakeWalk(float walkDuration)
    //{
    //    float walkRemaining = walkDuration;

    //    while (walkRemaining > 0)
    //    {
    //        Vector2 movement = new Vector2(walkingSpeed, _body.velocity.y);
    //        _body.velocity = movement;
    //        walkRemaining -= Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
