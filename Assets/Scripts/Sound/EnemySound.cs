using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip[] AttackClips;
    public AudioClip[] HurtClips;


    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.SetScheduledEndTime(0.1f);

    }


    public void onAttack()
    {
        if (!source.isPlaying) {
        Debug.Log("hereAttack");
            int r = Random.Range(0, AttackClips.Length);
            source.clip = AttackClips[r];
            source.Play();
        }

    }

    public void onHurt()
    {
        if (!source.isPlaying)
        {
        Debug.Log("hereHurt");
            int r = Random.Range(0, HurtClips.Length);
            source.clip = HurtClips[r];
            source.Play();
        }
    }
}

