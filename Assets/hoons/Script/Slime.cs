using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public float slime_distance = 3.0f;
    private float slime_to_player_distance;
    Rigidbody rb;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        slime_to_player_distance = Vector3.Distance(player.transform.position, transform.position);
        if(slime_to_player_distance < slime_distance)
        {
            
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            
        }
        else
        {
            agent.isStopped= false;
            agent.SetDestination(player.position);
        }
 
    }
   

}
