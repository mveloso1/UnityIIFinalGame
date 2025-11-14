using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public string triggerTag;
    public BehaviorScript behaviorScript;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(triggerTag))
        behaviorScript.Trigger();
    }
}
