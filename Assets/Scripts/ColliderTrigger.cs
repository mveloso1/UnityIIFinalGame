using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public string triggerTag = "";
    public GameObject[] triggerObj;
    public BehaviorScript behaviorScript;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(triggerTag))
        behaviorScript.Trigger();
        //else
        //    foreach (var obj in triggerObj)
        //    {
        //        if(obj == other)
        //        {
        //            behaviorScript.Trigger();
        //        }
        //    }
    }
}
