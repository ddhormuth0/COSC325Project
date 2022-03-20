using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float maxTimeValue = 90f;
    public Text timeText;
    private bool timesUp;
    private float timeValue;

    private void Start()
    {
        timeValue = maxTimeValue + 3;
        timesUp = false;
    }


    // Update is called once per frame
    void Update()
    {
        
        //pause at beginning
        if(timeValue <= maxTimeValue)
        {
            //checks if timer is more than 0
            if (timeValue > 0)
            {
                //subtracts the time value from current time passed
                timeValue -= Time.deltaTime;
            }

            else
            {
                //if time is up set time value to 0 and timeup to true
                timeValue = 0;
                timesUp = true;
            }
            DisplayTime(timeValue);
        }
        else
        {
            DisplayTime(maxTimeValue);
            timeValue -= Time.deltaTime;
        }
        
    }

    //displays the time value
    void DisplayTime(float timeDisplay)
    {
        if(timeDisplay < 0)
        {
            timeDisplay = 0;
        }
        //converts seconds left to minutes and seconds
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        //string format for time
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool getTimeUP()
    {
        return timesUp;
    }
}
