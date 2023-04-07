using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokebombManager : MonoBehaviour
{

    public Collider smokecollider;
    public FieldOfView fieldofview;
    public Collider suscollider;
    public AudioSource Landing;
    public AudioSource Smokesound;


    void Awake()
    {
        // destroy bomb 20 seconds after throwing it
        Destroy(this.gameObject, 15f);
        Invoke("SmokecolliderEnabler", 0.5f);
        smokecollider = transform.GetChild(1).GetComponent<Collider>();
        fieldofview = GameObject.FindObjectOfType(typeof(FieldOfView)) as FieldOfView;
        suscollider = transform.GetChild(2).GetComponent<Collider>();
        Landing = transform.GetChild(3).GetComponent<AudioSource>();
        Smokesound = transform.GetChild(4).GetComponent<AudioSource>();
    }

    public void SmokecolliderEnabler()
    {
        smokecollider.enabled = true;
        Smokesound.Play();

    }

    public void OnDestroy()
    {
        
        fieldofview.radius = 10f;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 2 && collision.collider.tag != "Player")
        {
            Debug.Log(collision.collider.tag);
            suscollider.enabled = true;
            Landing.Play();
            Debug.Log("boing");
        }
    }
}
