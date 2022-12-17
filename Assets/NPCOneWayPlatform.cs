using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    [SerializeField] private BoxCollider2D _playerCollider;

    public void Drop()
    {
        if (currentOneWayPlatform != null)
        {
            //Debug.Log($"{gameObject.name}: DROPPING!");
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(platformCollider, _playerCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(platformCollider, _playerCollider, false);

    }
}
