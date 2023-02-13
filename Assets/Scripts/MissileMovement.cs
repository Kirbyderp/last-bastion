using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    private Vector3 velocity, gravDirection;
    private float life, xBound = 9, zBound = 7;
    
    //Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up * .25f);
        velocity = new Vector3(3 * Mathf.Cos(Mathf.PI / 180 * (360 - transform.rotation.eulerAngles.y)), 0,
                               3 * Mathf.Sin(Mathf.PI / 180 * (360 - transform.rotation.eulerAngles.y)));
        life = Time.realtimeSinceStartup;
    }

    //Update is called once per frame
    void Update()
    {
        //If missile is live, determine gravity and apply it to velocity, then move missile.
        if (Time.realtimeSinceStartup - life < 10)
        {
            float gravConstant = 45f * Time.deltaTime / Mathf.Pow(calcDistance(), 4);
            gravDirection = new Vector3(-transform.position.x * gravConstant, 0,
                                        -transform.position.z * gravConstant);
            velocity += gravDirection;
        }
        transform.Translate(velocity * Time.deltaTime, Space.World);

        //Set rotation of missile so that it always faces the direction it travels
        float becauseArcTrigBounds = Mathf.Sign(velocity.z);
        if (becauseArcTrigBounds >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 360 - (180 / Mathf.PI
                                                  * Mathf.Acos(velocity.x / velocity.magnitude)), -90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 360 - (180 + 180 / Mathf.PI
                                                  * Mathf.Acos(-velocity.x / velocity.magnitude)), -90);
        }
        

        //Destroy missile if it hits planet
        if (calcDistance() < 2)
        {
            Destroy(gameObject);
            PlayerController.decrementMissileCount();
        }
        
        //Destroy missile if it goes out of bounds
        if (Mathf.Abs(transform.position.x) > xBound | Mathf.Abs(transform.position.z) > zBound)
        {
            Destroy(gameObject);
            PlayerController.decrementMissileCount();
        }
    }

    //Destroy missile and enemy if they collide and increment kill count for score purposes
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
        PlayerController.decrementMissileCount();
        ScoreTracker.incrementKill();
    }

    //Calculates distance from the origin
    private float calcDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
    }
}
