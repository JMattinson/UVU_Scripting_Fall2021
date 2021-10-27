using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("Stats")]
    public float moveSpeed; //Move speed in units/second
    public float jumpForce; //upward jump height

    public int curHP, maxHP; //health & max possible health

    [Header("Mouse Control")]
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
    
    public void TakeDamage(int damage)//applies damage to the player
    {
        curHP -= damage;
        if(curHP <= 0)
            Die();
    }

    void Die()//ends the game, when player's out of health
    {
        
    }



    void Move() // player movement controls
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        //rb.velocity = new Vector3(x, rb.velocity.y, z); old move command, doesn't rotate orientation
       // new movement, direction relative to camera
        Vector3 dir = transform.right * x + transform.forward * z;

        //adds force to general movement(WASD)
        dir.y = rb.velocity.y;
        rb.velocity = dir;
        
        
        
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(ray, 1.1f))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }
    public void GiveHealth(int amountToGive)
    {
       curHP = Mathf.Clamp(curHP + amountToGive , 0, maxHP); 

    }
    public void GiveAmmo(int amountToGive)
    {
       weapon.curAmmo = Mathf.Clamp(weapon.curAmmo + amountToGive , 0, weapon.maxAmmo); 
       
    }

    void CamLook ()// mouse aim controls
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;

        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        camera.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;

        

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
        if (Input.GetButton("Jump"))
            Jump();

    }


}
