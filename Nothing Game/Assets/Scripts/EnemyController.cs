using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    public float speed = 5.0f;  //acceleration

    void Start()
    {

    }

    void Update()
    {
//      
        transform.LookAt(Player);

        //if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        //{
        transform.position += transform.forward * speed * Time.deltaTime;
        //if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        //{
        //Here Call any function U want Like Shoot at here or something
        //}

        //}
    }
}