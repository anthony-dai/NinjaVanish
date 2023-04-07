using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class continue_button_script : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(false);
        Invoke("setAct", 5f);
    }

    public void setAct()
    {
        transform.gameObject.SetActive(true);
    }

    public void nextScreen()
    {
        Invoke("Go_to_next", 0.3f);
    }
    
    public void Go_to_next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
