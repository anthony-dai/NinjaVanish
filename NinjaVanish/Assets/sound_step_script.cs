using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_step_script: MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip clip;
    public float volume=0.5f;
    public float volume2=0f;
    public float volume3=0.5f;

    private void run()
    {
        audioSource.PlayOneShot(audioSource.clip, volume2);
    }
    private void step()
    {
        audioSource.PlayOneShot(audioSource.clip, volume);
    }
    private void throwing()
    {
        audioSource2.PlayOneShot(audioSource2.clip, volume);
    }
}

