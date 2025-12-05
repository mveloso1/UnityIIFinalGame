using UnityEngine;

public class AdversaryProjectileScript : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //transform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        
        Vector3 direction = transform.rotation * Vector3.forward;
        rb.AddForce(direction * 10f, ForceMode.Impulse);
        Debug.Log(direction);
        transform.position += direction * 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Damage player
            Debug.Log("DAMAGED PLAYER");
            //other.GetComponent<>
        }
        //if(! other.CompareTag("Enemy"))
        //Destroy(gameObject);
    }

}
