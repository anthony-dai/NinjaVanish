using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource walking;

    public void PlayWalking()
    {
        walking.Play();
    } 
    void OnCollisionEnter()
    {
        PlayWalking();
    }


}
