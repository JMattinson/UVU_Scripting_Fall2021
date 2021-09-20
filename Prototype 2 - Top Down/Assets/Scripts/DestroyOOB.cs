using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    //restraint parameters
    public float xRange = 11.0f;
    public float yRange = 4.5f;


    // Update is called once per frame
    void Update()
    {
         if(transform.position.x < -xRange )
        {
            Destroy(gameObject);
        }

         if(transform.position.x > xRange )
        {
            Destroy(gameObject);
        }

         if(transform.position.y < -yRange )
        {
           Destroy(gameObject);
        }

         if(transform.position.y > yRange )
        {
            Destroy(gameObject);
        }
    }
}
