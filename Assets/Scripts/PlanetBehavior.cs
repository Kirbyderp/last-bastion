using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    public Material planetColor, gameOverColor;
    public TMPro.TextMeshPro gameOverText;
    private float colorTimer;
    private static bool playDeath, lightTrigger;
    private static readonly float[,] planetColors = {{0, 124f/255, 1},
                                                     {89f/255, 124f/255, 166f/255},
                                                     {124f/255, 118f/255, 131f/255},
                                                     {159f/255, 111f/255, 91f/255},
                                                     {210f/255, 91f/255, 43f/255}};
    
    // Start is called before the first frame update
    void Start()
    {
        //Reset altered material colors
        planetColor.SetColor("_Color", new Color(0, 124f / 255, 1));
        gameOverColor.SetColor("_Color", new Color(0, 0, 0, 0));
        gameOverText.faceColor = new Color(1, 1, 1, 0);
        playDeath = false;
        lightTrigger = true;
        colorTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        //Plays death animation if planet dies
        if (playDeath)
        {
            float expandRate = 3 * Time.deltaTime;
            if (transform.localScale.y < 10)
            {
                transform.localScale += new Vector3(expandRate, expandRate, expandRate);
            }
            else if (transform.localScale.x < 20)
            {
                transform.localScale += new Vector3(expandRate, 0, expandRate);
            }
            else if (planetColor.color.r > 1)
            {
                colorTimer += Time.deltaTime;
                if (lightTrigger)
                {
                    LightRotater.triggerDeathLight();
                    lightTrigger = false;
                }
                planetColor.SetColor("_Color", new Color(3 - colorTimer, 0, 0));
            }
            else if (gameOverColor.color.a < 1)
            {
                colorTimer += Time.deltaTime;
                gameOverColor.SetColor("_Color", new Color(0, 0, 0, .5f * (colorTimer - 2)));
                gameOverText.faceColor = new Color(1, 1, 1, .5f * (colorTimer - 2));
            }
            else
            {
                playDeath = false;
            }
        }
    }

    //Updates the look of the planet when it is damaged
    public void damagePlanet(int currentDamage)
    {
        planetColor.SetColor("_Color", new Color(planetColors[currentDamage, 0],
                                                 planetColors[currentDamage, 1],
                                                 planetColors[currentDamage, 2]));
    }

    //Preps the planet death animation
    public void planetDeath()
    {
        planetColor.SetColor("_Color", new Color(3, 0, 0));
        playDeath = true;
        ScoreTracker.planetDeath();
        PlayerController.planetDeath();
    }
}
