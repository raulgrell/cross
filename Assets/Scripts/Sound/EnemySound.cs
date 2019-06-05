using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip[] AttackClips;
    public AudioClip[] HurtClips;
    public AudioClip[] DeathClip;


    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

    }


    public void onAttack()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
            int r = Random.Range(0, AttackClips.Length);
        source.PlayOneShot(AttackClips[r]);

    }

    public void onHurt()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
            int r = Random.Range(0, HurtClips.Length);
        source.PlayOneShot(HurtClips[r]);
        
    }
    public void onDeath()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        int r = Random.Range(0, DeathClip.Length);
        source.PlayOneShot(DeathClip[r]);
    }
    public void onActualDeath()
    {
        Destroy(gameObject);
    }
}

