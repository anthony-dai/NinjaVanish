using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockmanager : MonoBehaviour
{

    public AudioSource Landing;
    public Collider noisecollider;


    void Awake()
    {
        // destroy rock 10 seconds after throwing it
        Destroy(this.gameObject, 10f);
        Landing = transform.GetChild(1).GetComponent<AudioSource>();
        noisecollider = transform.GetChild(0).GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 2 && collision.collider.tag != "Player")
        {
            noisecollider.enabled = true;
            Landing.Play();
        }
    }
}
