using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 m_Movement;

    private GameObject tutorial;
    private GameObject pauseMenuUI;
    private GameObject mainMenuItems;
    private GameObject in_Game;

    private AudioSource victory_horns;

    private int count;
    private int levelIndex;

    private string addSummaryURL = "https://ninjavanish.000webhostapp.com/store_level_summary.php";

    Quaternion m_Rotation = Quaternion.identity;
    public float SprintSpeedMult = 2.0f;
    public float turnSpeed = 20f;
    public float speed = 1.0f;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator playerAnimator;
    [HideInInspector] public bool isSprinting;
    [HideInInspector] public bool isWalking;
    [HideInInspector] public Collider walkingCollider;
    [HideInInspector] public Collider sprintingCollider;
    [HideInInspector] public Collider throwCollider;
    [HideInInspector] public Throwscript throwscript;

    static public int score;
    static public float timer;

    public static bool GameIsPaused = false;

    public Text scoreText;
	
	public DifficultyScaler difScaler;
    
    void Start()
    {
        // Assign the Rigidbody component to our public rb variable
        rb = GetComponent<Rigidbody>();
        timer = 0;

        playerAnimator = GetComponent<Animator>();

        throwscript = GetComponent<Throwscript>();

        // Get collider components of walking and sprinting collider gameobject
        walkingCollider = transform.GetChild(2).GetComponent<Collider>();
        sprintingCollider = transform.GetChild(3).GetComponent<Collider>();
        throwCollider = transform.GetChild(7).GetComponent<Collider>();
        throwCollider.enabled = false;
        sprintingCollider.enabled = false;
        walkingCollider.enabled = true;
        isSprinting = false;

        //Find UI scripts
        mainMenuItems = GameObject.Find("In-Game_UI");
        pauseMenuUI = mainMenuItems.transform.Find("Pause_Menu").gameObject;
        in_Game = mainMenuItems.transform.Find("In_game").gameObject;

        //If player is in tutorail level find UI scripts
        if (SceneManager.GetActiveScene().name == "Tutorial_1")
        {
            tutorial = mainMenuItems.transform.Find("Tutorial_screen").gameObject;
            victory_horns = GameObject.Find("victory-horns").GetComponent<AudioSource>();
        }


        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        // Set the count to zero 
        count = 0;

        // Raise error if missing
        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause_Menu not in hierarchy or not active");
        }
        if (scoreText == null)
        {
            Debug.LogError("TimerText not in hierarchy or not active");
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        // SetCountText();
        if (GameObject.Find("DifficultyScaler")) difScaler = GameObject.Find("DifficultyScaler").GetComponent<DifficultyScaler>();
    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement = Quaternion.AngleAxis(135, Vector3.up) * m_Movement;
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (throwscript.throwing)
        {
            m_Movement.Set(0f, 0f, 0f);
            turnSpeed = 1f;
            isWalking = false;
            throwCollider.enabled = true;

        } else
        {
            turnSpeed = 20f;
            throwCollider.enabled = false;

        }


        // No animation, later when animation is added replace deltatime with deltaPosition to account for root motion
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            rb.velocity = m_Movement * speed * SprintSpeedMult;
            isSprinting = true;
        }
        else
        {
            rb.velocity = m_Movement * speed;
            isSprinting = false;
        }

        playerAnimator.SetBool("isSprinting", isSprinting);
        playerAnimator.SetBool("isWalking", isWalking);

        // Determining when to enable walking/sprinting colliders based on isWalking, isSprinting booleans
        if (!isWalking)
        {
            walkingCollider.enabled = false;
            sprintingCollider.enabled = false;
        }
        else if (!isSprinting)
        {
            walkingCollider.enabled = true;
            sprintingCollider.enabled = false;
        }
        else
        {
            walkingCollider.enabled = false;
            sprintingCollider.enabled = true;
        }
        SetScoreText();
        rb.MoveRotation(m_Rotation);
		timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);


            //load next scene for early acces
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            //SetCountText();
        }


        if (other.gameObject.CompareTag("Next_Level_Trigger"))
        {   
            // Send summary to server
            StartCoroutine(PostSummary(SceneManager.GetActiveScene().name, 0, timer.ToString("n1")));

            //Load new scene
            score++;
            if (difScaler) difScaler.levelsPassed++;
            SetScoreText();

            if (SceneManager.GetActiveScene().name == "Tutorial_1")
            {
                Time.timeScale = 0f;
                in_Game.SetActive(false);
                Debug.Log("tutorial");
                tutorial.SetActive(true);
                victory_horns.Play();
            }
            else
            {
                load_level();
            }



        }
    }

    private void load_level()
    {
        while (true)
        {
            levelIndex = Random.Range(5, SceneManager.sceneCountInBuildSettings - 1);
            if (levelIndex != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(levelIndex);
                break;
            }
        }
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

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}

