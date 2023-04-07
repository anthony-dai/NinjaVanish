using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips_displayer : MonoBehaviour
{
    public GameObject Tip1;


    // Start is called before the first frame update
    void Start()
    {
        Tip1 = GameObject.Find("Tip_1");
        Tip1.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip1.SetActive(true);
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip1.SetActive(false);
        }

    }

}
