using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform player;
    public float startSpeed = 1;
    
    public List<GameObject> points;
    NavMeshAgent agent;
    Animator animator;

    int index = 0;

    float DistanciaPlayer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
        
    }

    void Update()
    {
         DistanciaPlayer = Vector3.Distance(player.transform.position, transform.position);

        
        if (DistanciaPlayer < 5)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = 3f;
            if ( DistanciaPlayer <= 1)
            {
                KillPlayer();
            }
        }
        else 
        {
            agent.SetDestination(points[index].transform.position);
            agent.speed = startSpeed;
        }
        
        animator.SetFloat("Speed", agent.speed);
    }

    void ChangePoint()
    {
        index++;

        if (index >= points.Count)
        {
            index = 0;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            ChangePoint();
        }
        
    }



    private void KillPlayer()
    {
        animator.SetTrigger("Attack");
        
    }

    
}
