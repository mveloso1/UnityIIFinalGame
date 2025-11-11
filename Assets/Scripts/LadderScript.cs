using UnityEngine;
using UnityEngine.InputSystem;

public class LadderScript : MonoBehaviour
{
    public Transform playerController;
    bool insideLadder;
    public float ladderSpeed = 3.5f;
    public FPMovement playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            playerInput.enabled = false;
            insideLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            playerInput.enabled = true;
            insideLadder = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (insideLadder && Input.GetKey(KeyCode.W))
        {
            playerController.transform.position += Vector3.up * ladderSpeed * Time.deltaTime;
        }
    }
}
