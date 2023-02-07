using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBehavior : MonoBehaviour
{
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(-transform.position.x / transform.position.magnitude, 0,
                               -transform.position.z / transform.position.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);

        //Destroys enemy if it reaches planet
        if (transform.position.magnitude < 2)
        {
            Destroy(gameObject);
        }
    }
}
