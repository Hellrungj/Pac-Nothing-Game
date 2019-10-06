using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    public float speed = 2.0f;  //acceleration

    public UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {

    }

    void Update()
    {      
        //transform.LookAt(Player);

        //transform.position += transform.forward * speed * Time.deltaTime;

        agent.SetDestination(Player.position);

    }

//    void OnCollisionEnter(Collision col)
//    {
//        if (col.gameObject.CompareTag("Wall"))
//        {
//            transform.position += transform.forward * 0 * Time.deltaTime;
//        }
//    }

}