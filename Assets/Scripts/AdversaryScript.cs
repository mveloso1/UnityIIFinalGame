using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AdversaryScript : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public int arenaSize = 16;
    public bool charge = true;
    public bool move = true;

    public float moveSpeed = 3f, chargeSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!charge && move)
            agent.SetDestination(player.position);
        else if (charge)
        {
            StartCoroutine(Charge());
            charge = false;
            move = false;
        }
    }

    IEnumerator Charge()
    {
        Debug.Log("COROUTINE STARTED");
        //// Set destination to move away from the player
        //Vector3 newPos = Vector3.MoveTowards(transform.position, player.position, -1);
        //newPos.y = 0;
        //newPos = newPos.normalized * arenaSize / 2;

        //RaycastHit hit;

        //Physics.Raycast(transform.position + new Vector3(0, 5, 0),
        //    Vector3.RotateTowards(transform.position, player.position, 2, 0), out hit, 20f);
        //Vector3 newPos = hit.point;
        //agent.SetDestination(newPos);
        //newPos.y = transform.position.y;

        


        //Debug.Log(hit.point);
        //yield return new WaitUntil(() => Vector3.Distance(transform.position, newPos) <= 2);

        //agent.speed = 0;

        // Set charge destination

        //newPos = Vector3.MoveTowards(transform.position, player.position, 1);
        //newPos.y = 0;
        //newPos = newPos.normalized * -arenaSize / 2;
        //agent.SetDestination(newPos);
        //Debug.Log(newPos);

        //Physics.Raycast(transform.position + new Vector3(0, 5, 0),
        //    Vector3.RotateTowards(transform.position, player.position, 2, 1), out hit, 20f);
        //newPos = hit.point;
        //agent.SetDestination(newPos);
        //newPos.y = transform.position.y;


        // Pause before charging
        yield return new WaitForSeconds(1.5f);

        // Charge
        //agent.speed = chargeSpeed;
        //// Wait until edge has been reached
        //yield return new WaitUntil(() => Vector3.Distance(transform.position, newPos) <= 2);
        //agent.speed = moveSpeed;
        //yield return new WaitForSeconds(1.5f);
        move = true;
    }
}
