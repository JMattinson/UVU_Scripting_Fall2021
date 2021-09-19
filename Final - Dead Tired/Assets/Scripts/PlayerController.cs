using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    private bool run = false;
    private bool aim = false;

    public float hInput;
    public float vInput;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
// sprint check
    if (Input.GetKey(KeyCode.LeftShift))
    {
        run = true;
    } else run = false;
// aim check
    if (Input.GetKey(KeyCode.K))
    {
        aim = true;
    } else aim = false;

        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        // movement, left/right
        
        //run modifier
        if (run && !aim) 
        {speed = 5.0f; }
        else if (aim) 
        {speed = 0.0f; }
        else speed = 2.5f;

        transform.Rotate(Vector3.up, turnSpeed* hInput * Time.deltaTime );
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
        

    }
}
