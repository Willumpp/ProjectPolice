using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMenu : MonoBehaviour
{
    //Audio
    public AudioSource audioSourceButtonPress;

    void Start()
    {
        audioSourceButtonPress = GameObject.FindGameObjectWithTag("ButtonPressAudioSource").GetComponent<AudioSource>();
    }

    public void ActiveMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void DisableMenu(GameObject menu)
    {
        audioSourceButtonPress.Play();
        menu.SetActive(false);
    }
}
