using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class RaycastFromPlayer : MonoBehaviour
{
    public float raycastDistance = 5.0f;
    bool holdingItem = false;
    GameObject heldObj;
    float cooldown;
    public float cooldownLength;

    
    public Animator templeDoor;

    bool templeDoorUnlocked = false;

    public AudioClip puzzleSolve;
    private AudioSource audioSource;

    MeshRenderer hitObj;

    //These are only for the tutorial areas ONLY
    public GameObject wallButtonMessageBox;
    public GameObject pickupMessageBox;

    //public GameObject bow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cooldown = cooldownLength;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        //// Make pickup item glow if possible
        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3.0f))
        {
            Scene currentScene = SceneManager.GetActiveScene(); 

            if (hit.collider.tag == "PickupItem" && !holdingItem) // for regular pickup items
            {
               hitObj = hit.collider.GetComponent<MeshRenderer>();
               hitObj.materials[1].SetFloat("_Scale", 1.03f);

                if (currentScene.name == "TutorialRoom")
                {
                    pickupMessageBox.SetActive(true); // only displays in tutorial room
                }
            }
            if (hit.collider.tag == "WallButton") // for tutorial room only
            {
                hitObj = hit.collider.GetComponent<MeshRenderer>();
                hitObj.materials[1].SetFloat("_Scale", 1.03f);
                wallButtonMessageBox.SetActive(true);
            }


        }
        else // Remove glow when not not hovered over
        {
            if (hitObj != null)
            {
                hitObj.materials[1].SetFloat("_Scale", 1.0f);
                hitObj = null;
                if (wallButtonMessageBox.activeSelf)
                {
                    wallButtonMessageBox.SetActive(false);
                }
                if (pickupMessageBox.activeSelf)
                {
                    pickupMessageBox.SetActive(false); 
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
           // 
           //if(hit.collider.gameObject.TryGetComponent<BehaviorOnShot>(out BehaviorOnShot behaviorScript))
           if(hit.collider.gameObject.TryGetComponent<BehaviorScript>(out BehaviorScript behaviorScript))
            {
                if(behaviorScript.playerShot)
                {
                    behaviorScript.Trigger();
                    Debug.Log(hit.collider.name + " was hit successfully!");
                }
            }
            else
                Debug.Log(hit.collider.name + " was hit, did not trigger!");

            //if (hit.collider.CompareTag("DoorButton") && doorUnlocked)
            //{
            //    leftDoor.SetTrigger("OpenDoor");
            //    rightDoor.SetTrigger("OpenDoor");
            //}
            //if (hit.collider.CompareTag("Archer"))
            //{
            //    //toggle 
            //    bow.SetActive(true);
            //}
        
        }
    }

    public void ShootBow(InputAction.CallbackContext ctx)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 40f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // Cooldown if statement
                if(cooldown <= 0f)
                {
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(5f);
                    cooldown = cooldownLength;
                }
            }
            //else
                //Debug.Log(hit.collider.name + " was hit, did not trigger!");
        }
    }
}
