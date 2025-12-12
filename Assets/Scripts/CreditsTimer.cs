using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    public float delay = 8f;         // time before switching scenes
    public int sceneToLoad = 0;      // build index OR replace with string for scene name

    private void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }
    
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}