using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private int levelIndex;
    
    public void StartGame()
    {
        levelIndex = Random.Range(5, SceneManager.sceneCountInBuildSettings - 1);
        SceneManager.LoadScene(levelIndex);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(4);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Options()
    {
        //TODO
    }

    public void Help()
    {
        //TODO
    }
   
}
