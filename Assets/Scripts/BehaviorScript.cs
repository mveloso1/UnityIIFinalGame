using UnityEngine;

public class BehaviorScript : MonoBehaviour
{
    [Header("Accepted Triggers")]
    public bool playerInteract, playerShot;

    [Header("Trigger Scripts")]
    [SerializeField] RemoveObjBehavior removeObjScript;
    [SerializeField] MoveObjBehavior moveObjScript;

    

    public void Trigger()
    {
        if (removeObjScript != null)
        {
            removeObjScript.RemoveObjects();
        }
        if (moveObjScript != null)
        {
            moveObjScript.MoveObj();
        }
    }
}
