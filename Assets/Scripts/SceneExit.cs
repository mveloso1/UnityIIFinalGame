using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If we entered something tagged "Exit"
        if (other.CompareTag("Exit"))
        {
            // Load the next scene in build order
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }
    }
}
