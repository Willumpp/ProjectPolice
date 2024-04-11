using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider sfxSlider;
    float maxVolume;
    
    void Start()
    {
        maxVolume = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = maxVolume * sfxSlider.value;
    }
}
