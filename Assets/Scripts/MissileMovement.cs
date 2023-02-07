using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    private Vector3 velocity, gravDirection, gravVelocity = Vector3.zero;
    private float life;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up * .25f);
        velocity = 3 * Vector3.up;
        life = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //If missile is live, determine gravity and apply it to velocity, then move missile.
        if (Time.realtimeSinceStartup - life < 10)
        {
            float gravConstant = 4f * Time.deltaTime / Mathf.Pow(calcDistance(), 2);
            gravDirection = new Vector3(-transform.position.x * gravConstant, 0,
                                        -transform.position.z * gravConstant);
            gravVelocity += gravDirection;
        }
        transform.Translate(velocity * Time.deltaTime);
        transform.Translate(gravVelocity * Time.deltaTime, Space.World);


        //Destroy missile if it hits planet
        if (calcDistance() < 2)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    //Calculates distance from the origin
    private float calcDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
    }
}
