using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeypadScript : MonoBehaviour
{

    public string password;
    public string enteredpassword;
    public TMP_Text keypadDisplay;
    public int passDigits;

    public GameObject escapePod;
    public GameObject escapePodStand;
    public Camera cutSceneCamera;
    public Camera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        passDigits = password.Length;
        keypadDisplay.text = "Enter Code";
    }

    // Update is called once per frame
    void Update()
    {
        if (enteredpassword.Length == passDigits)
        {
            if(enteredpassword == password)
            {
                keypadDisplay.text = "Correct Passcode";
                playerCamera.enabled = false;
                cutSceneCamera.enabled = true;
                Destroy(escapePodStand);
                escapePod.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(loadEnd());
                

            }
            else
            {
                keypadDisplay.text = "Incorrect Passcode";
                enteredpassword = "";
            }
        }
    }

    public void ShowCursor(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ButtonNumber(string btnNum)
    {
        EnterCode(btnNum);
    }

    private void EnterCode(string btnNum)
    {
        enteredpassword += btnNum;
        keypadDisplay.text = enteredpassword;
    }

    IEnumerator loadEnd()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(2);
        this.gameObject.SetActive(false);
    }
}
