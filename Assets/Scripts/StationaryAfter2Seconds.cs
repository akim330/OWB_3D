using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryAfter2Seconds : MonoBehaviour
{
    private float timer;
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        timer = 2f;
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(_body);
            this.enabled = false;
        }
    }
}
