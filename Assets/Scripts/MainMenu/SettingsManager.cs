using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SettingsManager : MonoBehaviour
{
    private AudioSource source;
    private Slider[] sliders = new Slider[3];

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void changedMusic()
    {
        sliders = FindObjectsOfType<Slider>();
        foreach (Slider s in sliders)
        {
            if(s.transform.name == "SliderM")
            source.volume = s.value;
        }
    }
}
