using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Throwscript : MonoBehaviour
{
    public Rigidbody pfrock;
    public Rigidbody pfSmokebomb;
    public Transform Handposition;
    public float maxForce = 12f;
    public float holdDownTime;
    public bool throwing = false;
    public int amountOFRocks = 2;
    public int amountOfSmokebombs = 2;

    public RawImage SmokebombImage;
    public RawImage RockImage;
    public TextMeshProUGUI countRock;
    public TextMeshProUGUI countSmokebomb;


    public Animator playerAnimator;

    private float holdDownStartTime;
    private bool smokebomb = false;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        SmokebombImage = GameObject.Find("imageGrenade").GetComponent<RawImage>();
        RockImage = GameObject.Find("imageRock").GetComponent<RawImage>();
        countRock = GameObject.Find("counterRock").GetComponent<TextMeshProUGUI>();
        countSmokebomb = GameObject.Find("counterSmoke").GetComponent<TextMeshProUGUI>();

    }


    private void Update()
    {
        countRock.text = amountOFRocks.ToString();
        countSmokebomb.text = amountOfSmokebombs.ToString();


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            smokebomb = false;
            Color temp = RockImage.color;
            temp.a = 1f;
            RockImage.color = temp;

            Color temp2 = SmokebombImage.color;
            temp2.a = 0.2f;
            SmokebombImage.color = temp2;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            smokebomb = true;
            Color temp = RockImage.color;
            temp.a = 0.2f;
            RockImage.color = temp;

            Color temp2 = SmokebombImage.color;
            temp2.a = 1f;
            SmokebombImage.color = temp2;

        }


        if (Input.GetKeyDown(KeyCode.Space) && Munition_left() == false)
        {
            Debug.Log("No more rocks");
        }

        if (Input.GetKeyDown(KeyCode.Space) && Munition_left() == true)
        {
            //Space down, start time
            holdDownStartTime = Time.time;
            throwing = true;
        }


        if (Input.GetKey(KeyCode.Space) && Munition_left() == true)
        {
            holdDownTime = Time.time - holdDownStartTime;
        }


        if (Input.GetKeyUp(KeyCode.Space) && Munition_left() == true)
        {
            holdDownTime = Time.time - holdDownStartTime;

            Rigidbody obj;

            if (smokebomb == false)
            {
                obj = Instantiate(pfrock, Handposition.position, Handposition.rotation);
                amountOFRocks--;
            }
            else
            {
                obj = Instantiate(pfSmokebomb, Handposition.position, Handposition.rotation);
                amountOfSmokebombs--;
            }


            obj.velocity = Handposition.transform.up * CalculateHoldDownForce(holdDownTime);
            throwing = false;
        }

        playerAnimator.SetBool("isThrowing", throwing);
    }

    private void FixedUpdate()
    {
        if (amountOFRocks == 0)
        {
            Color32 redColor = new Color32(255, 0, 0, 63);
            RockImage.color = redColor;
        }

        if (amountOfSmokebombs == 0)
        {
            Color32 redColor = new Color32(255, 0, 0, 63);
            SmokebombImage.color = redColor;
        }
    }

    public float CalculateHoldDownForce(float holdtime)
    {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdtime / maxForceHoldDownTime);
        float force = holdTimeNormalized * maxForce;
        return force;
    }

    public bool Munition_left()
    {
        if (smokebomb == true && amountOfSmokebombs == 0)
        {
            return false;
        }
        else if (smokebomb == false && amountOFRocks == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
