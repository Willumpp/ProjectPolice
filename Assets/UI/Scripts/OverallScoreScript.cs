using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallScoreScript : MonoBehaviour
{
    public Text self;

    // Update is called once per frame
    void Update()
    {
        self = gameObject.GetComponent<Text>();
        self.text = "Overall Score: " + PlayerPrefs.GetInt("OverallScore", 0).ToString();

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerPrefs.SetInt("OverallScore", 1000000000);
        }
    }
}
