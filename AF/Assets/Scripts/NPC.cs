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

    [SerializeField] bool amigo;
   [SerializeField] public bool final;

    public GameObject npc;
    public Material FriendMaterial;

    float DistanciaPlayer;


    public Transform endingpos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
        GameManager.INSTANCE.PlayerPegouCristal.AddListener(MudaparaAmigo);
    }

    void Update()
    {
         DistanciaPlayer = Vector3.Distance(player.transform.position, transform.position);

        
        if (DistanciaPlayer < 5 && !final)
        {
            agent.SetDestination(player.transform.position);
            agent.speed = 3f;
            if (!amigo && DistanciaPlayer <= 1)
            {
                KillPlayer();
            }
        }
        else if(!amigo)
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
        else if (other.CompareTag("End") && amigo)
        {
            final = true;
            Door door = other.GetComponent<Door>();
            agent.SetDestination(door.destination);
        }
    }

    private void MudaparaAmigo()
    {
        amigo = true;
        Renderer npcrend = npc.GetComponent<Renderer>();
        npcrend.material = FriendMaterial;
    }

    private void OnDestroy()
    {
        GameManager.INSTANCE.PlayerPegouCristal.RemoveListener(MudaparaAmigo);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void KillPlayer()
    {
        Attack();
        GameManager.INSTANCE.PlayerDeath.Invoke();
    }

    public void Dance()
    {
        agent.Warp(endingpos.position);
        animator.SetTrigger("Dance");
    }
}
