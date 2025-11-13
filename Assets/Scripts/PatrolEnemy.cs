using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    bool arrived;
    bool patrolling;
    int destination;
    public Transform eye;
    public Transform target;
    Vector3 lastPosition;
    public float viewDistance = 10.0f;
    public float viewAngle = 90.0f;
    public LayerMask playerMask;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolling = true;
        lastPosition = transform.position;

        
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(eye.position, target.position) < viewDistance)
        {
            Vector3 playerDirection = (target.position - eye.position).normalized;
            float angleDifference = Vector3.Angle(eye.forward, playerDirection);
            if(angleDifference < viewAngle / 2)
            {
                if (Physics.Linecast(eye.position, target.position, ~playerMask))
                {
                    lastPosition = target.position;
                    return true;
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending)
        {
            return;
        }
        //Code for patrolling
        if (patrolling)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!arrived)
                {
                    arrived = true;
                    //start coroutine
                    StartCoroutine(GoToNextPatrolPoint());
                }
            }
        }
        else
        {
            arrived = false;
        }
        // when enemy sees target
        if (CanSeePlayer())
        {
            agent.SetDestination(target.position);
            patrolling = false;
            // setup attack
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            if(!patrolling)
            {
                anim.SetBool("Attack", false);
                agent.SetDestination(lastPosition);
                if(agent.remainingDistance < agent.stoppingDistance)
                {
                    patrolling = true;
                    StartCoroutine(GoToNextPatrolPoint());
                }
            }
        }
        //play move animation 
        anim.SetFloat("Moving", agent.velocity.sqrMagnitude);
    }

    IEnumerator GoToNextPatrolPoint()
    {
        if (waypoints.Length == 0)
        {
            yield break;
        }

        patrolling = true;
        yield return new WaitForSeconds(2.0f);
        arrived = false;
        agent.destination = waypoints[destination].position;
        destination = (destination + 1) % waypoints.Length;
    }
}
