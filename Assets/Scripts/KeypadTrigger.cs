using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{

    public GameObject keyPadUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            keyPadUI.SetActive(true);
            other.GetComponent<MouseLook>().enabled = false;
            other.transform.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keyPadUI.SetActive(false);
            other.GetComponent<MouseLook>().enabled = true;
            other.transform.Find("Main Camera").GetComponent<MouseLook>().enabled = true;
        }
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
