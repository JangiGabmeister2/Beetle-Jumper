using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float timeLeft;
    public bool timerOn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {
                Debug.Log("No Time Left");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
