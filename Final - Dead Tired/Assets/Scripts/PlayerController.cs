using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float hInput;
    public float vInput;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    
    void FixedUpdate()
    {
        MoveMod();
        PlayerMove();
    }

    void PlayerMove () //Manages player movement proper, is modified by MoveMod()
    {
        // movement, left/right
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, turnSpeed* hInput * Time.deltaTime );
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
        
    }

    void MoveMod () //Checks if anything's changing player speed, Using AimingCheck() & RunCheck()
    {
        if (RunCheck() && !AimingCheck()) 
        {speed = 5.0f; }
        else if (AimingCheck()) 
        {speed = 0.0f; }
        else speed = 2.5f;
    }
    public bool AimingCheck ()
    {
        if (Input.GetKey(KeyCode.K))
        return true;
        else
        return false;
    }

    public bool RunCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        return true;
        else 
        return false;

    }


}
