using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    private float xBound = 12, zBound = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > xBound | Mathf.Abs(transform.position.z) > zBound)
        {
            Destroy(gameObject);
        }
    }
}
