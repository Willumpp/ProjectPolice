using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool menuActive = false;
    public bool hasPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive == false)
            {
                menuActive = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                menuActive = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
