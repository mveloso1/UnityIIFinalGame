using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fallDelay = 0.3f;       // Time before platform starts falling
    public float destroyDelay = 5f;    // Time before platform is destroyed or reset

    private Rigidbody rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Make sure the platform doesn't fall until triggered
        if (rb != null)
            rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasFallen && other.CompareTag("Player"))
        {
            StartCoroutine(FallAfterDelay());
        }
    }

    IEnumerator FallAfterDelay()
    {
        hasFallen = true;
        yield return new WaitForSeconds(fallDelay);

        if (rb != null)
        {
            rb.isKinematic = false; // Allow physics to make it fall
        }

        // Optionally destroy it or reset after some time
        Destroy(gameObject, destroyDelay);
    }
}
