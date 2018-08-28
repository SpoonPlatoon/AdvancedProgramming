using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include Artificial Intellegence of API
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public enum State
    {
        Patrol,
        Seek
    }

    public int Health
    {
        get
        {
            return health;
        }
    }

    public State currentState = State.Patrol;

    public NavMeshAgent agent;
    public Transform target;
    public Transform waypointParent;
    public float distanceToWaypoint = 1f;
    public float detectionRadius = 5f;

    public FieldOfView fov;
    public AudioSource alertSound;
    public GameObject alertSymbol;

    private int health = 100;
    private Transform[] waypoints;
    private int currentIndex = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }

    IEnumerator SeekDelay()
    {
        yield return new WaitForSeconds(1f);
        currentState = State.Patrol;
        target = null;
    }

    void Patrol()
    {
        alertSymbol.SetActive(false);//Gameobject

        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 1;
        }

        Transform point = waypoints[currentIndex];

        float distance = Vector3.Distance(transform.position, point.position);
        if (distance <= distanceToWaypoint)
        {
            currentIndex++;
        }

        agent.SetDestination(point.position);

        if(fov.visibleTargets.Count > 0)
        {
            //Set the target to the first one
            target = fov.visibleTargets[0];
            //Switch to seek state
            currentState = State.Seek;
            //Detected
            alertSound.Play(); //Audiosource
            alertSymbol.SetActive(true);//Gameobject
        }
        



    }

    void Seek()
    {
        //update the AI's target position
        agent.SetDestination(target.position);

        //get distance to target
        float distToTarget = Vector3.Distance(transform.position, target.position);
        //if distance to target is greater than detection radius (out of range)
        if (distToTarget >= detectionRadius)
        {
            StartCoroutine(SeekDelay());
        }
    }

    // Update is called once per frame
    void Update ()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Seek:
                Seek();
                break;
            default:
                break;
        }
    }

    //void FixedUpdate()
    //{
    //    Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
    //    foreach (var hit in hits)
    //    {
    //        Player player = hit.GetComponent<Player>();
    //        if (player)
    //        {
    //            target = player.transform;
    //            return;
    //        }
    //    }

    //    target = null;
    //}

    public void DealDamage(int damageDealt)
    {
        health -= damageDealt;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}


