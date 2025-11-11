using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float speed = 1.0f;
    public float distance = 3.0f;
    public float startTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distMoved = Mathf.PingPong(Time.time - startTime, distance / speed);
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, distMoved / distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
