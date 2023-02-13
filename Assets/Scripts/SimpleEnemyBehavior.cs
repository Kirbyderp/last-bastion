using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBehavior : MonoBehaviour
{
    private Vector3 velocity;
    public static int planetDamage = 0, planetMaxHealth = 5;
    public PlanetBehavior planet;
    
    
    //Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(-transform.position.x / transform.position.magnitude, 0,
                               -transform.position.z / transform.position.magnitude);
    }

    //Update is called once per frame
    void Update()
    {
        //Moves enemy towards planet
        transform.Translate(velocity * Time.deltaTime, Space.World);

        //Destroys self and damages or kills planet if it reaches planet
        if (transform.position.magnitude < 2)
        {
            Destroy(gameObject);
            planetDamage++;
            if (planetDamage < planetMaxHealth)
            {
                planet.damagePlanet(planetDamage);
            }
            else if (planetDamage == planetMaxHealth)
            {
                planet.planetDeath();
            }
        }
    }
}
