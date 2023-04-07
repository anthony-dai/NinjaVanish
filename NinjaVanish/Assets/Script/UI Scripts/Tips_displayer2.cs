using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips_displayer2 : MonoBehaviour
{
    public GameObject Tip2;


    // Start is called before the first frame update
    void Start()
    {
        Tip2 = GameObject.Find("Tip_2");
        Tip2.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip2.SetActive(true);
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip2.SetActive(false);
        }

    }

}
