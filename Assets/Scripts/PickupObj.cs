using UnityEngine;

public class PickupObj : MonoBehaviour
{
    bool pickUp;
    Rigidbody rb;
    public Transform destinationObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup()
    {
        pickUp = !pickUp;

        if (pickUp)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.position = destinationObj.position;
            transform.parent = destinationObj.transform;
        }
        else
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            transform.parent = null;
        }
    }
}
