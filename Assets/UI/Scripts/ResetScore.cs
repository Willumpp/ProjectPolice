using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore : MonoBehaviour
{
    //Audio
    public AudioSource audioSourcebuttonPress;

    public void ResetScores()
    {
        audioSourcebuttonPress.Play();
        PlayerPrefs.DeleteAll();
    }
}
