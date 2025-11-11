using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyJump : MonoBehaviour
{

    [SerializeField]
    float jumpHeight = 2.0f;
    [SerializeField]
    float durationTime = 0.5f;
    public Animator anim;
    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                anim.SetTrigger("Jump");
                yield return StartCoroutine(Jump(agent, jumpHeight, durationTime));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    } 

    IEnumerator Jump(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data =  agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos;

        float time = 0.0f;

        while (time < 1.0f)
        {
            float upDist = height * (time - time * time);
            agent.transform.position = Vector3.Lerp(startPos, endPos, time) + upDist * Vector3.up;

            time += Time.deltaTime / duration;
            yield return null;
        }
    }
}
