using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips_displayer4 : MonoBehaviour
{
    public GameObject Tip4;


    // Start is called before the first frame update
    void Start()
    {
        Tip4 = GameObject.Find("Tip_4");
        Tip4.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip4.SetActive(true);
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tip4.SetActive(false);
        }

    }

}
