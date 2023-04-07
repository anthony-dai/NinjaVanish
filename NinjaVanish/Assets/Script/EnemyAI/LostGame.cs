using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LostGame : MonoBehaviour
{
    public GameObject loseMenu;
    // Start is called before the first frame update
    void Start()
    {
        loseMenu = GameObject.Find("LoseMenu");
        loseMenu.SetActive(false);
    }

    public void LostTheGame()
    {
        Time.timeScale = 0f;
        loseMenu.SetActive(true);
    }

}
