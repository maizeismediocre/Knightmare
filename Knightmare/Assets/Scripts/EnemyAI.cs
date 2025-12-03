using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    // this script is based off the enemy AI script from the scifi game assignment
    Animator anim;      // the animator
    NavMeshAgent na;    // the nav mesh agent

    public Transform target;        // the target (which will be the player)
    
    public List<Transform> waypoints; // list of waypoints for patrolling
    private int currentWaypointIndex = 0; // current waypoint index

    // Use this for initialization
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        MoveToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect the player
        float enemydist = Vector3.Distance(target.transform.position, transform.position);
        anim.SetFloat("EnemyDist", enemydist);

        float speed = na.velocity.magnitude;
        anim.SetFloat("Speed", speed);
        
      




        // Determine which state the guard is in
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);
        
        // If guard is in Attack state
        if (asi.IsName("Attack"))
        {
            na.isStopped = true;
           
        }
        // If guard is in run state
        else if (asi.IsName("Running"))
        {
            
            na.SetDestination(target.position);
            na.isStopped = false;
            //increase the speed of the navmesh agent
            na.speed = 8;

        }



        // If guard is in start state, patrol waypoints
        else if (asi.IsName("Patrol"))
        {
            na.speed = 3;
            if (!na.pathPending && na.remainingDistance < 0.5f)
            {
                MoveToNextWaypoint();

                
            }
            na.isStopped = false;
        }
    }

    // Move to the next waypoint in the list
    void MoveToNextWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        na.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        na.isStopped = false;
    }

    //if a player enters the trigger they lose a life
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GamePlay.Instance.EnemyAttack();
            

        }
    }






}