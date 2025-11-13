using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    public GameObject enemy;
    public Transform player;
    Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        transform.position = enemy.transform.position + offset;
    }
}
