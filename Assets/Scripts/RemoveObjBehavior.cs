using UnityEngine;

public class RemoveObjBehavior : MonoBehaviour
{
    public GameObject[] objectsToRemove;

    public void RemoveObjects()
    {
        foreach (GameObject obj in objectsToRemove)
        {
            Destroy(obj);
        }
    }
}
