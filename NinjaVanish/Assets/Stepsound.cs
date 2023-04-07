using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stepsound : MonoBehaviour
{
    public AudioSource audioSource;

    

    private void step()
        {
            audioSource.Play();
        }

}

