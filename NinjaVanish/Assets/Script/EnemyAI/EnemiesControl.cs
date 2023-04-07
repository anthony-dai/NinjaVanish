using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesControl : MonoBehaviour
{
    public GameObject[] enemies;
    public bool moreAlert
    { get; set; }
    private bool moreAlertOn;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If DifficultyScaler GameObject exists in current scene then adjust enemy speed accordingly.
        if (GameObject.Find("DifficultyScaler"))
        {
            int levelsPassed = GameObject.Find("DifficultyScaler").GetComponent<DifficultyScaler>().levelsPassed;
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyStats enemyStat = enemies[i].GetComponent<EnemyStats>();
                enemyStat.difficulty += 0.1f * (Mathf.Floor(levelsPassed / 3));
                enemyStat.setSpeed(enemyStat.difficulty);
            }
        }
        moreAlert = false;
        moreAlertOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moreAlert && !moreAlertOn)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyStats enemyStat = enemies[i].GetComponent<EnemyStats>();
                enemyStat.difficulty += 0.1f;
                enemyStat.setSpeed(enemyStat.difficulty);
            }
            moreAlertOn = true;
        }
    }
}
