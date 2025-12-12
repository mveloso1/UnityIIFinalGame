using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class CreditsTimer : MonoBehaviour
{
    public float timeRemaining;
    int minutes;
    int seconds;
    public TMP_Text timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = 10;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void CountDown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            minutes = Mathf.FloorToInt(timeRemaining / 60f);
            seconds = Mathf.FloorToInt(timeRemaining % 60f);

            if (seconds < 10)
            {
                timer.text = $"Time left {minutes}:0{seconds}";
            }
            else
            {
                timer.text = $"Time left {minutes}:{seconds}";
            }
        }
        else
        {
            timeRemaining = 0;
            timer.text = "Time left 0:00";
            SceneManager.LoadScene(1);
        }
    }

    private void TimeIsUp()
    {
        //Game over
        Debug.Log("Time is up!");
    }
}
