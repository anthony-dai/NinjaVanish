using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInteractions : MonoBehaviour
{
    // Constant running function that is running regardless of state

    public StateController controller;
    public pauseScript lostGame;
    private bool isPlayer;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        isPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<StateController>();
        lostGame = GameObject.Find("PauseScript").GetComponent<pauseScript>();
    }

    private void Update()
    {
        if (isPlayer)
        {
            controller.IsHeard = true;
            controller.susLocation = player.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sus"))
        {
            controller.IsHeard = true;
            controller.susLocation = other.transform.position;
            if (other.transform.parent != null)
            {
                if (other.transform.parent.CompareTag("Player")) isPlayer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayer = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameObject.Find("DifficultyScaler"))
            {
                DifficultyScaler difScaler = GameObject.Find("DifficultyScaler").GetComponent<DifficultyScaler>();
                difScaler.levelsPassed = 0;
            }
            lostGame.LostTheGame();
        }
    }
}
