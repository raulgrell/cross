using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private AudioSource audio;
    private Slider[] sliders = new Slider[3];

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void changedMusic()
    {
        sliders = FindObjectsOfType<Slider>();
        Debug.Log(sliders[0].value);
    }
}
