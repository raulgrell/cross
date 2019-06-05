using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbientSoundController : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

}
