using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInDisable : MonoBehaviour
{
    public GameObject fadeInPanel;
    void Start()
    {
        fadeInPanel.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
