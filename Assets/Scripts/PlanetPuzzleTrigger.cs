using UnityEngine;

public class PlanetPuzzleTrigger : MonoBehaviour
{
    public GameObject target;
    //public PlanetPuzzleController puzzleController;
    public bool triggerActive = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            triggerActive = true;
            Debug.Log("Trigger activated!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            triggerActive = false;
        }
    }
}
