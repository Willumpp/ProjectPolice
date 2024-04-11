using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public static bool inCountdown = false;
    public AfkTimer afkTimer;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        afkTimer = GameObject.FindGameObjectWithTag("Player").GetComponent<AfkTimer>();
        Timer.stopTimer = true;
        inCountdown = true;
        text = gameObject.GetComponent<Text>();
        text.text = "3";
    }

    public void Text3()
    {
        text.text = "3";
    }

    public void Text2()
    {
        text.text = "2";
    }

    public void Text1()
    {
        text.text = "1";
    }

    public void TextGo()
    {
        text.text = "GO!";
    }

    public void AfterGo()
    {
        gameObject.SetActive(false);
        Timer.stopTimer = false;
        inCountdown = false;
        afkTimer.enabled = true;
    }
}
