using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed; //Move speed in units/second
    public float jumpForce; //upward jump height

    public float lookSensitivity; //Mouse sensitivity
    public float maxLookX;  //lowest rotation for camera
    public float minLookX;  //hightest rotation for camera
    
    private float rotX; //Current X rotation of the camera
    private Camera camera;
    private Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        //get the camera and rigidbody
        camera = camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        rb.velocity = new Vector3(x, rb.velocity.y, z);
    }

    void CamLook ()
    {
        float y = Input.GetAxis("Mouse x") * lookSensitivity;
        rotX += Input.GetAxis("Mouse y") *lookSensitivity;
        

    }
}
