using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class RaycastFromPlayer : MonoBehaviour
{
    public float raycastDistance = 5.0f;
    bool holdingItem = false;
    GameObject heldObj;

    public bool redBox = false;
    public bool blueBox = false;
    public bool prism = false;

    public GameObject doorButton;
    public Animator leftDoor;
    public Animator rightDoor;
    public Animator templeDoor;

    bool doorUnlocked = false;
    bool templeDoorUnlocked = false;

    public AudioClip puzzleSolve;
    private AudioSource audioSource;

    MeshRenderer hitObj;
    public GameObject messageBox;
    public GameObject bow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);

        if(redBox && blueBox) //&& prism
        {
           // doorButton.GetComponent<Renderer>().material.color = Color.green;
            doorUnlocked = true;
            templeDoorUnlocked = true;
           // AudioSource.PlayClipAtPoint(puzzleSolve, transform.position);

        }
        else
        {
            //doorButton.GetComponent<Renderer>().material.color = Color.red;
            doorUnlocked = false;
            templeDoorUnlocked = false;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3.0f))
        {
            if (hit.collider.tag == "PickupItem" && !holdingItem)
            {
                hitObj = hit.collider.GetComponent<MeshRenderer>();
                hitObj.materials[1].SetFloat("_Scale", 1.03f);
            }
            if(hit.collider.tag == "DoorButton" && doorUnlocked)
            {
                hitObj = hit.collider.GetComponent<MeshRenderer>();
                hitObj.materials[1].SetFloat("_Scale", 1.03f);
                messageBox.SetActive(true);
            }
            else if (hit.collider.tag == "Archer")
            {
                messageBox.SetActive(true);
            }
            else
            {
                messageBox.SetActive(false);
            }
        }
        else
        {
            if (hitObj != null)
            {
                hitObj.materials[1].SetFloat("_Scale", 1.0f);
                hitObj = null;
                if(messageBox.activeSelf)
                {
                    messageBox.SetActive(false);
                }
            }
        }

    }

    public void PickupItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("PickupItem"))
                {
                    hit.collider.GetComponent<PickupObj>().Pickup();
                    heldObj = hit.collider.gameObject;
                    holdingItem = true;
                }
            }
        }
        if (ctx.canceled)
        {
            if(holdingItem)
            {
                heldObj.GetComponent<PickupObj>().Pickup();
                holdingItem = false;
                heldObj = null;
            }
        }
    }

    public void InteractableObject(InputAction.CallbackContext ctx)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
           
            if (hit.collider.CompareTag("DoorButton") && doorUnlocked)
            {
                leftDoor.SetTrigger("OpenDoor");
                rightDoor.SetTrigger("OpenDoor");
            }
            if (hit.collider.CompareTag("Archer"))
            {
                //toggle 
                bow.SetActive(true);
            }
        }
    }
}
