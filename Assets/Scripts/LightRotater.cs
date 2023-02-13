using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotater : MonoBehaviour
{
    private static bool deathLight;
    public Light lightComp;
    
    //Start is called before the first frame update
    void Start()
    {
        deathLight = false;
    }

    //Update is called once per frame
    void Update()
    {
        if (!deathLight)
        {
            transform.Rotate(Vector3.up * .6f * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            lightComp.intensity -= Time.deltaTime * .25f;
        }
    }

    //Sets deathLight to true, which will move the directional light to assist the game over animation
    public static void triggerDeathLight()
    {
        deathLight = true;
    }


}
