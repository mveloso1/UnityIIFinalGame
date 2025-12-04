using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        //loads tutorial
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PuzzleRoom()
    {
        SceneManager.LoadScene(2);
    }

    public void AdversaryRoom()
    {
        SceneManager.LoadScene(3);
    }
    public void PlatformRoom()
    {
        SceneManager.LoadScene(4);
    }
    public void EscapeRoom()
    {
        SceneManager.LoadScene(5);
    }
    public void Win()
    {
        SceneManager.LoadScene(6);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(7);
    }

}
