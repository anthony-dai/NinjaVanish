using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips_displayer3 : MonoBehaviour
{
    public GameObject Tip3;


    // Start is called before the first frame update
    void Start()
    {
        Tip3 = GameObject.Find("Tip_3");
        Tip3.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip3.SetActive(true);
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip3.SetActive(false);
        }

    }

}
