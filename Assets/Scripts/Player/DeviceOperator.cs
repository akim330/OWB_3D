using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                OperableDevice device = collider.GetComponent<OperableDevice>();

                if (device != null)
                {
                    device.Operate();
                }

                //Vector3 direction = collider.transform.position - transform.position;
                //if (Vector3.Dot(direction, transform.forward) > 0.5f)
                //{
                //    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                //}
            }
        }

    }
}
