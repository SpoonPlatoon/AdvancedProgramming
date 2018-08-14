using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include Artificial Intellegence of API
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public int Health
    {
        get
        {
            return health;
        }
    }

    public NavMeshAgent agent;
    public Transform target;
    public Transform waypointParent;
    public float distanceToWaypoint = 1f;

    public bool loop = false;
    private bool pingPong = false;

    private int health = 100;
    private Transform[] waypoints;
    private int currentIndex = 1;


    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (target) //if target is set
        {
            //update the AI's target position
            agent.SetDestination(target.position);
        }
        else
        {
            //if currentIndex exceeds size of array
            if(currentIndex >= waypoints.Length)
            {
                if (loop)
                {
                    //reset back to "first" element
                    currentIndex = 1;
                }
                else
                {
                    //cap the index if its greater
                    currentIndex = waypoints.Length - 1;
                    //reverse it
                    pingPong = true;
                }
            }

            if (currentIndex <= 0)
            {
                if (loop)
                {
                    //reset back to "first" element
                    currentIndex = waypoints.Length - 1;
                }
                else
                {
                    //cap the index if its greater
                    currentIndex = 1;
                    //reverse it
                    pingPong = false;
                }
            }

            Transform point = waypoints[currentIndex];

            float distance = Vector3.Distance(transform.position, point.position);
            if(distance <= distanceToWaypoint)
            {
                if (pingPong)
                {
                    //move to previous waypoint
                    currentIndex--;
                }
                else
                {
                    currentIndex++;
                }
            }

            //set new target to next waypoint
            agent.SetDestination(point.position);

        }
	}

    public void DealDamage(int damageDealt)
    {
        health -= damageDealt;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
