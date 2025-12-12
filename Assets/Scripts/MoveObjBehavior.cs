using Unity.VisualScripting;
using UnityEngine;

public class MoveObjBehavior : MonoBehaviour
{
    [Header("Values")]
    public Vector3 moveAmount;
    private Vector3 startPos, endPos;
    public float moveDuration;
    private float moveTime;
    public bool retrigger, active, canTrigger;

    public AudioClip moveStone;
    private AudioSource audioSource;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveAmount;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(active)
        {
            moveTime += Time.deltaTime;
            if( moveTime < moveDuration )
                transform.position = Vector3.Lerp(startPos, endPos, moveTime/moveDuration);
            else
            {
                transform.position = endPos;
                active = false;
            }
            if(retrigger)
            {
                Vector3 temp = startPos;
                startPos = endPos;
                endPos = temp;
                canTrigger = true;
            }
        }
    }



    public bool MoveObj()
    {
        audioSource.PlayOneShot(moveStone);
        bool triggerStatus = canTrigger;
        if (canTrigger)
        {
            active = true;
            moveTime = 0;
        }
        canTrigger = false;
        return triggerStatus;

    }
}
