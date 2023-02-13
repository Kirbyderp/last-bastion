using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private float timeScore;
    private int dispScore;
    private static bool isAlive;
    private static float timeOfDeath, deathTimer;
    private static int numKills;
    public TMPro.TextMeshPro dispText;

    //Start is called before the first frame update
    void Start()
    {
        timeScore = 0;
        dispScore = 0;
        numKills = 0;
        timeOfDeath = 0;
        deathTimer = 0;
        isAlive = true;
        InvokeRepeating("UpdateTimeScore", 0, 1);
    }

    //Update is called once per frame
    void Update()
    {
        //Calculates current score, then updates the displayed text appropriately
        dispScore = (int)(timeScore) + 20 * numKills;
        
        if (!isAlive)
        {
            deathTimer += Time.deltaTime;
        }

        if (deathTimer < timeOfDeath + 4)
        {
            dispText.text = "Score\n" + dispScore;
        }
        else
        {
            dispText.text = "Game Over!\nScore\n" + dispScore;
        }
    }

    //Updates the time component of the score while the player hasn't game over'd
    void UpdateTimeScore()
    {
        if (isAlive)
        {
            timeScore = Time.realtimeSinceStartup * Time.realtimeSinceStartup / 120;
        }
    }

    //For score purposes, increments the number of kills the player made by 1
    public static void incrementKill()
    {
        numKills++;
    }

    //Called by PlanetBehavior, assigns certain variable values to assist in the game over animation
    public static void planetDeath()
    {
        isAlive = false;
        timeOfDeath = Time.realtimeSinceStartup;
        deathTimer = Time.realtimeSinceStartup;
    }
}
