using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target; //Assign player here
    [SerializeField] Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField] float sensitivity = 2.0f;
    [SerializeField] float minY = -35f;
    [SerializeField] float maxY = 60f;
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] PlayerInput playerInput;

    float yaw;
    float pitch;
    InputAction lookAction;
    Vector3 currentVelocity;
    Vector3 currentPosition;
    Quaternion currentRotation;

    private void Awake()
    {
        if (playerInput == null && target != null)
        {
            playerInput = target.GetComponent<PlayerInput>();
        }
        if(playerInput != null)
        {
            lookAction = playerInput.actions["Look"];
        }

        currentPosition = transform.position;
        currentRotation = transform.rotation;
    }

 
    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 lookInput = lookAction != null ? lookAction.ReadValue<Vector2>(): Vector2.zero;

        yaw += lookInput.x * sensitivity;
        pitch -= lookInput.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = target.position + targetRotation * offset;

        //Smooth position
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, 1 - Mathf.Exp(-smoothTime * Time.deltaTime * 60f));
        //Smooth rotation
        currentRotation = Quaternion.Slerp(currentRotation, Quaternion.LookRotation(target.position - currentPosition), 1 - Mathf.Exp(-smoothTime * Time.deltaTime * 60f));

        transform.position = currentPosition;
        transform.rotation = currentRotation;
    }
}
