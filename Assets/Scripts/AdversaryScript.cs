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
    public float turnSpeed;

    public Transform[] waypoints;

    public string state;
    public float stateTimer;
    float attackTimer = 0f;
    float attackLength = 53f / 30f + 7f/30f;

    public float moveSpeed;

    public Animator modelAnimator;
    //public float overshootAccel = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "Chase";
        stateTimer = 5f;
        
        turnSpeed = agent.angularSpeed;
        moveSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Chase after player
        if (state == "Chase" || state == "Attacking")
        {
            // 
            agent.SetDestination(player.position);

            // If close to player, perform melee attack
            //if (state == "Attacking")
            //{
            //    if (attackTimer > 0f)
            //        attackTimer -= Time.deltaTime;
            //    else
            //        state = "Chase";
            //}
            if (Vector3.Distance(transform.position, player.position) < 1f)
            {
                modelAnimator.SetBool("Attacking", true);
                //state = "Attacking";
                //attackTimer = attackLength;
            }
            // Decrease time to firing projectile
            else
            {
                stateTimer -= Time.deltaTime;
                modelAnimator.SetBool("Attacking", false);
            }
            // If timer is up, prepare to shoot projectile

            // TEST
            Vector3 velocity = agent.velocity;
            if (velocity.sqrMagnitude > 0.01f)
            {
                //Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            if (stateTimer < 0)
            {
                state = "PrepareShoot";
                agent.SetDestination(ChooseWaypoint());
                
                //agent.speed = moveSpeed * 1.5f;
            }
        }
        else if (state == "PrepareShoot")
        {
            // If at waypoint, begin to shoot projectile
            if(Vector3.Distance(transform.position, agent.destination) < 2f)
            {
                state = "Shoot";
                agent.speed = 0f;
                agent.velocity = Vector3.zero;
                modelAnimator.SetTrigger("Magic");
                //agent.angularSpeed *= 1000;
                // Timer for shooting projectile
                stateTimer = 2f;
            }
        }
        else if (state == "Shoot")
        {
            // Point towards player
            agent.SetDestination(player.position);
            Vector3 temp = Vector3.RotateTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), 20f, 99f);
            temp.y = 0f;
            transform.rotation = Quaternion.LookRotation(temp, Vector3.up);

            stateTimer -= Time.deltaTime;
            if(stateTimer < 0)
            {
                // Fire projectile
                //GameObject proj = Instantiate(projectilePrefab, transform.position, transform.rotation);
                
                stateTimer = 5f;
                state = "Chase";
                agent.speed = moveSpeed;
                agent.angularSpeed = turnSpeed;
            }
        }
            // Animation
            modelAnimator.SetFloat("Moving", agent.velocity.sqrMagnitude);
    }

    private Vector3 ChooseWaypoint()
    {
        float dist = 0;
        Vector3 target = Vector3.zero;
        foreach (Transform t in waypoints)
        {
            if(Vector3.Distance(t.position, transform.position) < dist)
                if(Vector3.Distance(t.position, player.position) > 5)
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
