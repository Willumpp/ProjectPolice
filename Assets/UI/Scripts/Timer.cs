using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public static bool stopTimer = false;
    private float startTime;
    public static float endTime;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        startTime = Time.time + 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTimer == false)
        {
            time = Time.time - startTime; //Subtract from start time
        }

        string hours = ((int)time / 3600).ToString(); //Convert seconds to hours
        string minutes = ((int) time / 60).ToString(); //Convert seconds to minutes
        string seconds = (time % 60).ToString("f2"); //Get milliseconds

        if (stopTimer == false)
        {
            timeText.text = $"{hours}:{minutes}:{seconds}"; //Update timer text
        }
        else
        {
            endTime = time;
        }
    }
}
