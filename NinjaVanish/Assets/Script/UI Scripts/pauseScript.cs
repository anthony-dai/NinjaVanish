using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;



public class pauseScript : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;
    public static bool Dead = false;
    public GameObject LoseMenuUI;
    private string addSummaryURL = "https://ninjavanish.000webhostapp.com/store_level_summary.php";


    // Start is called before the first frame update
    void Start()
    {

        LoseMenuUI.SetActive(false);

         // Raise error if missing
        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause_Menu not in hierarchy or not active");
        }
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Dead = false;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Dead)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            
        }
    }


    public void Resume()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Quit()
    {
        PlayerMovement.score = 0;
        SceneManager.LoadScene("Start Menu");
    }

    public void TryAgain()
    {
        PlayerMovement.score = 0;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        pauseMenuUI.SetActive(false);
        Dead = false;
        Time.timeScale = 1f;
    }

    public void ToHighscore()
    {
        SceneManager.LoadScene("End Screen");
    }

    public void LostTheGame()
    {
        Time.timeScale = 0f;
        Dead = true;
        LoseMenuUI.SetActive(true);
        StartCoroutine(PostSummary(SceneManager.GetActiveScene().name, 1, PlayerMovement.timer.ToString("n1")));
    }
    IEnumerator PostSummary(string LevelName, int Died, string LevelTimer)
    {
        WWWForm form = new WWWForm();
        form.AddField("Level_Name", LevelName);
        form.AddField("Died", Died);
        form.AddField("Level_Timer", LevelTimer);

        UnityWebRequest UWR = UnityWebRequest.Post(addSummaryURL, form);
        yield return UWR.SendWebRequest();

        if (UWR.error != null)
        {
            Debug.Log("There was an error posting level summary: " + UWR.error);
        }
        else
        {
            Debug.Log("Summary upload complete! Trying to load next scene...");
        }
    }
}
