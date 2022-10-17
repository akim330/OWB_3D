using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour
{
    [SerializeField] private int health;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void OnHit(int damage)
    {

        health -= damage;
        Debug.Log($"Hit for damage {damage}! {health}");


        if (health <= 0)
        {
            Dislodge();
        }
        else
        {
        }
    }

    public void Dislodge()
    {
        Debug.Log("Dislodged!");

    }
}
