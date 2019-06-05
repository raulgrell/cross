using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SettingsManager : MonoBehaviour
{
    private AudioSource source;
    private Slider[] sliders = new Slider[3];

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
    }

    public void changedMusic()
    {
        sliders = FindObjectsOfType<Slider>();
        source.volume = sliders[0].value;
    }
}
