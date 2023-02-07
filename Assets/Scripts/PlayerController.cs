using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float aimInput, speedInput, aimBound = 120, aimSpeed = 200;
    public GameObject aimArrow;
    public GameObject missilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Updates position to orbit around the planet.
        transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * -9);

        //Allows player to aim with the a/d or left/right keys
        aimInput = Input.GetAxis("Aim");
        speedInput = Input.GetAxis("Speed");
        aimArrow.transform.RotateAround(transform.position, Vector3.up,
                                        Time.deltaTime * aimInput * Mathf.Pow(2, speedInput) * aimSpeed);



        //Keeps aim within a certain bound
        float angleDif;
        if (aimArrow.transform.rotation.eulerAngles.y > transform.rotation.eulerAngles.y)
        {
            angleDif = aimArrow.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
        }
        else
        {
            angleDif = 360 + aimArrow.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
        }
        
        if (angleDif > 260)
        {
            angleDif -= 360;
        }
        
        if (angleDif < 45 - aimBound)
        {
            aimArrow.transform.RotateAround(transform.position, Vector3.up,
                                            Time.deltaTime * -aimInput * Mathf.Pow(2, speedInput) * aimSpeed);
        }
        else if (angleDif > 45 + aimBound)
        {
            aimArrow.transform.RotateAround(transform.position, Vector3.up,
                                            Time.deltaTime * -aimInput * Mathf.Pow(2, speedInput) * aimSpeed);
        }
        
        //Create a missile when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float xPosDif = 4 * (aimArrow.transform.position.x - transform.position.x);
            if (xPosDif > 1)
            {
                xPosDif = 1;
            }
            else if (xPosDif < -1)
            {
                xPosDif = -1;
            }
            float zPosDif = aimArrow.transform.position.z - transform.position.z;
            Instantiate(missilePrefab, aimArrow.transform.position,
                        Quaternion.Euler(0, Mathf.Acos(xPosDif) * 180 / Mathf.PI *
                        -Mathf.Sign(Mathf.Asin(zPosDif)), -90));
        }
    }
}
