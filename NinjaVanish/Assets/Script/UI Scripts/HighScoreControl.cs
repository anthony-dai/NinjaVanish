using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreControl : MonoBehaviour
{
    private string addScoreURL= "https://ninjavanish.000webhostapp.com/addscore.php";
    private string highscoreURL = "https://ninjavanish.000webhostapp.com/display_hs.php";
    public Text scoreText;
    public Text nameTextInput;
    public Text scoreResultText;

    public void Start()
    {
        scoreText.text = "Your Score: " + PlayerMovement.score.ToString();
        StartCoroutine(GetScores());
    }

    public void GetScoreBtn()
    {
        StartCoroutine(GetScores());
    }

    public void SendScoreBtn()
    {
        StartCoroutine(PostScores(nameTextInput.text, PlayerMovement.score.ToString()));
        nameTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
    }

    IEnumerator GetScores()
    {
        Debug.Log("Attempting to retrieve scores");
        UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL);
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
        {
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
            scoreResultText.text = "There was an error retreiving the Highscore";
        }
        else
        {
            string dataText = hs_get.downloadHandler.text.Replace("<br>","\n");
            Debug.Log(dataText);
            scoreResultText.text = dataText;

        }
    }

    IEnumerator PostScores(string name, string score)
    {
        WWWForm form = new WWWForm();
        form.AddField("newName", name);
        form.AddField("newScore", score);

        UnityWebRequest UWR = UnityWebRequest.Post(addScoreURL, form);
        yield return UWR.SendWebRequest();

        if (UWR.error != null)
        {
            Debug.Log("There was an error posting the new score: " + UWR.error);
        }
        else
        {
            Debug.Log("Form upload complete! Trying to update scoreboard...");
            StartCoroutine(GetScores());
        }
    }

    public void HomeScreen()
    {
        PlayerMovement.score = 0;
        SceneManager.LoadScene("Start Menu");
    }

}
