using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCBehaviorPattern
{
    Random
}

public enum NPCState
{
    Walk,
    Stand
}

public class NPCMovement : MonoBehaviour
{
    public NPCBehaviorPattern state;
    [SerializeField] private float minWalk;
    [SerializeField] private float walkingSpeed;
    private float _walked;

    private Rigidbody2D _body;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (state == NPCBehaviorPattern.Random)
        {
            if (_walked >= minWalk)
            {

            }
        }
    }

    

    IEnumerator TakeWalk(float walkDuration)
    {
        float walkRemaining = walkDuration;

        while (walkRemaining > 0)
        {
            Vector2 movement = new Vector2(walkingSpeed, _body.velocity.y);
            _body.velocity = movement;
            walkRemaining -= Time.deltaTime;
            yield return null;
        }
    }
}
