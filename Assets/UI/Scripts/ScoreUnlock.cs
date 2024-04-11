using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUnlock : MonoBehaviour
{
    public int carPrice = 2000;
    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("OverallScore", 0) > carPrice)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }
}
