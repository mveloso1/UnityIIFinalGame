using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AdversaryScript : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public int arenaSize = 32;

    public GameObject projectilePrefab;
    //public bool charge = true;
    public float turnSpeed = 12f;

    public Transform[] waypoints;

    public string state;
    public float stateTimer;

    public float moveSpeed;
    //public float overshootAccel = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "Chase";
        stateTimer = 3f;
        moveSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Chase after player
        if (state == "Chase")
        {
            // 
            agent.SetDestination(player.position);
            
            // If close to player, perform melee attack
            if (Vector3.Distance(transform.position, player.position) < 1)
            {
                // Attack
            }
            // Decrease time to firing projectile
            else stateTimer -= Time.deltaTime;
            // If timer is up, prepare to shoot projectile
            if(stateTimer < 0)
            {
                state = "PrepareShoot";
                agent.SetDestination(ChooseWaypoint());
                
                agent.speed = moveSpeed * 1.5f;
            }
        }
        else if (state == "PrepareShoot")
        {
            // If at waypoint, begin to shoot projectile
            if(Vector3.Distance(transform.position, agent.destination) < 1f)
            {
                state = "Shoot";
                agent.speed = 0.05f;
                agent.angularSpeed *= 500;
                // Timer for shooting projectile
                stateTimer = 3f;
            }
        }
        else if (state == "Shoot")
        {
            // Point towards player
            agent.SetDestination(player.position);


            agent.updateRotation = true;
            //Vector3 temp = Vector3.RotateTowards(transform.position, player.position, 2, 0);
            //transform.Rotate(new Vector3(0f, temp.y, 0f));


            stateTimer -= Time.deltaTime;
            if(stateTimer < 0)
            {
                // Fire projectile
                GameObject proj = Instantiate(projectilePrefab, transform.position + Vector3.up, transform.rotation);
                
                stateTimer = 5f;
                state = "Chase";
                agent.speed = moveSpeed;
                agent.angularSpeed = turnSpeed;
            }
        }
        

    }

    private Vector3 ChooseWaypoint()
    {
        float dist = 0;
        Vector3 target = Vector3.zero;
        foreach (Transform t in waypoints)
        {
            if(Vector3.Distance(t.position, transform.position) < dist)
                if(Vector3.Distance(t.position, player.position) < 9)
                {
                    dist = Vector3.Distance(t.position, transform.position);
                    target = t.position;
                }
        }
        return target;
    }

    //IEnumerator Charge()
    //{
    //    Debug.Log("COROUTINE STARTED");
    //    //// Set destination to move away from the player
    //    //Vector3 newPos = Vector3.MoveTowards(transform.position, player.position, -1);
    //    //newPos.y = 0;
    //    //newPos = newPos.normalized * arenaSize / 2;

    //    //RaycastHit hit;

    //    //Physics.Raycast(transform.position + new Vector3(0, 5, 0),
    //    //    Vector3.RotateTowards(transform.position, player.position, 2, 0), out hit, 20f);
    //    //Vector3 newPos = hit.point;
    //    //agent.SetDestination(newPos);
    //    //newPos.y = transform.position.y;

        


    //    //Debug.Log(hit.point);
    //    //yield return new WaitUntil(() => Vector3.Distance(transform.position, newPos) <= 2);

    //    //agent.speed = 0;

    //    // Set charge destination

    //    //newPos = Vector3.MoveTowards(transform.position, player.position, 1);
    //    //newPos.y = 0;
    //    //newPos = newPos.normalized * -arenaSize / 2;
    //    //agent.SetDestination(newPos);
    //    //Debug.Log(newPos);

    //    //Physics.Raycast(transform.position + new Vector3(0, 5, 0),
    //    //    Vector3.RotateTowards(transform.position, player.position, 2, 1), out hit, 20f);
    //    //newPos = hit.point;
    //    //agent.SetDestination(newPos);
    //    //newPos.y = transform.position.y;


    //    // Pause before charging
    //    yield return new WaitForSeconds(1.5f);

    //    // Charge
    //    //agent.speed = chargeSpeed;
    //    //// Wait until edge has been reached
    //    //yield return new WaitUntil(() => Vector3.Distance(transform.position, newPos) <= 2);
    //    //agent.speed = moveSpeed;
    //    //yield return new WaitForSeconds(1.5f);
    //    move = true;
    //}
}
