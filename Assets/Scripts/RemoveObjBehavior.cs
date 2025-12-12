using UnityEngine;

public class RemoveObjBehavior : MonoBehaviour
{
    public GameObject[] objectsToRemove;

    public AudioClip destroy;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void RemoveObjects()
    {
        foreach (GameObject obj in objectsToRemove)
        {
            Destroy(obj);
            audioSource.PlayOneShot(destroy);
        }
    }
}
