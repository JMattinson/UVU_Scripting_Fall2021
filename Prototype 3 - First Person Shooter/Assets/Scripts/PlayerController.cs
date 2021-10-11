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

    private Weapon weapon;

    void Awake()
    {
        //grabbing the shooting script for ref.
        weapon = GetComponent<Weapon>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //get the camera and rigidbody
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook();
        if(Input.GetButton("Fire1"))
        {
            if(weapon.CanShoot())
                weapon.Shoot();
        }
        

    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();

    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        //rb.velocity = new Vector3(x, rb.velocity.y, z); old move command, doesn't rotate orientation
       
        Vector3 dir = transform.right * x + transform.forward * z;
        rb.velocity = dir;
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(ray, 1.1f))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void CamLook ()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;

        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        camera.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;

        

    }
}
