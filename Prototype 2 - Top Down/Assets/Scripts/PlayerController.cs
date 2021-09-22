using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f;
    public float turnSpeed = 200.0f;

   // public float health;

    //input parameters
    public float hInput;
    public float vInput;

    //restraint parameters
    public float xRange = 11.0f;
    public float yRange = 4.5f;

    public GameObject projectile;
    public Transform launcher;
    public Vector3 offset = new Vector3(0,1,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player controls, horizontal/vertical
        hInput = Input.GetAxis("Horizontal"); 
        vInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.back, turnSpeed * hInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * vInput * Time.deltaTime);

        //Player constraints, keeps them in the game area
        if(transform.position.x < -xRange )
        {
            transform.position = new Vector3 (-xRange,transform.position.y,transform.position.z);

        }

         if(transform.position.x > xRange )
        {
            transform.position = new Vector3 (xRange,transform.position.y,transform.position.z);

        }

         if(transform.position.y < -yRange )
        {
            transform.position = new Vector3 (transform.position.x, -yRange,transform.position.z);

        }

         if(transform.position.y > yRange )
        {
            transform.position = new Vector3 (transform.position.x, yRange,transform.position.z);

        }

        //shoot 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, launcher.transform.position, projectile.transform.rotation);
        }

    }
}
