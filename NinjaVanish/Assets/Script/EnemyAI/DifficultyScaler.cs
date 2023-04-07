using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    public int levelsPassed
    {get; set;}

    private void Awake()
    {
        levelsPassed = 0;
        DontDestroyOnLoad(this.gameObject);
    }
}
