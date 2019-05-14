using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private AudioSource audio;
    public Slider[] sliders;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void changedVolume()
    {
        audio.volume = sliders[0].value;
    }
}
