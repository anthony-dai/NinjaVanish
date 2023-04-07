using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        PlayerMovement.score = 0;
        SceneManager.LoadScene("Start Menu");
    }

    public void StartGame()
    {
        levelIndex = Random.Range(5, SceneManager.sceneCountInBuildSettings - 1);
        SceneManager.LoadScene(levelIndex);
    }
}
