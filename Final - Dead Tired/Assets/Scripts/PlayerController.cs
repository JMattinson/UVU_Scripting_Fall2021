using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float runSpeed;
    public float turnSpeed;
    public float hInput;
    public float vInput;
    public float sInput;

    private float lastQTurn;
    public float qTurnRate;

    [Header("Health")]
    public int curHP;
    public int maxHP;

    [Header("Lighting & Particles")]
    public GameObject lazer;

     private Weapon weapon;
     public GameManager GM;

     private LayerMask Ground;
    
    void Start()
    {
                //Initialize UI
        GameUI.instance.UpdateHealthBar(curHP);
        GameUI.instance.UpdateAmmoText(weapon.curAmmo,weapon.maxAmmo);
    }
        void Awake()
    {
       weapon = GetComponent<Weapon>();
       
       curHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
            Die();
        GameUI.instance.UpdateHealthBar(curHP);
    }
    public void Die()
    {
       GM.GameOver();
    }
    void FixedUpdate()
    {
        //calling the general movement checkers and code
        StrafeMod();
        MoveMod();
        PlayerMove();
        //If I'm aiming & shooting
         if(AimingCheck() && Input.GetButton("Shoot"))
        {
            //fire my weapon
            if(weapon.CanShoot())
                weapon.Shoot();
                GameUI.instance.UpdateAmmoText(weapon.curAmmo,weapon.maxAmmo);
        }

    }
    //Manages player movement proper, is modified by MoveMod()
    void PlayerMove () 
    {
        // movement, left/right
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");


        transform.Rotate(Vector3.up, turnSpeed* hInput * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
        transform.Translate(Vector3.right * speed * Time.deltaTime * sInput);

        //basic error handler, keeps the player from glitching up wall corners
        if(transform.position.y > 1)
            {
             transform.Translate(Vector3.down * speed * Time.deltaTime * 1);
            }
        
    }

    void MoveMod () //Checks if anything's changing player speed, Using AimingCheck() & RunCheck()
    {
        //is the player running, without aiming?
        if (RunCheck() && !AimingCheck() && vInput > 0) 
        //make move speed run speed
        {speed = runSpeed; }

        //is the player aiming?
        else if (AimingCheck()) 
        //slow player down
        {
        speed = 1.5f; 
        turnSpeed = 50;
        }
        //error handler
        else 
        {
            speed = 2.0f;
            turnSpeed = 100;
        }
        //is the player trying to quickturn?
        if(QuickTurn())
        {
            //pull a fast 180
            transform.Rotate(Vector3.up,-180);   
        }
        //turns on the lazer pointer
        if (AimingCheck())
            lazer.SetActive(true);
        else 
            lazer.SetActive(false);
    }
    public bool AimingCheck ()
    {
        //Is the aim key being pressed?(right bumper)
        if (Input.GetButton("Aim"))
        return true;
        else
        return false;
    }

    public bool RunCheck()
    {
        //is the run key being pressed? (bottom face button)
        if (Input.GetButton("Run"))
        return true;
        else 
        return false;

    }

    public void StrafeMod()
    {
        //are either of the strafing keys being pressed?
        if (Input.GetKey(KeyCode.Q))
        sInput = -1;
        else if (Input.GetKey(KeyCode.E))
        sInput = 1;
        else sInput = Input.GetAxis("Strafe");
    }
    public bool QuickTurn()
    {
        //this basic timer keeps the player from Spinning
        if (Input.GetButton("Qturn") && Time.time - lastQTurn >= qTurnRate)
       {
           //yes, player is qturning
           lastQTurn = Time.time;
           return true;
       }
        else
        //player isn't pressing quickturn 
        return false;
    }
    public void GiveHealth(int amountToGive)
    {
        //give the player the healthkit's set value, but limit to max health
       curHP = Mathf.Clamp(curHP + amountToGive , 0, maxHP);
       GameUI.instance.UpdateHealthBar(curHP);

    }
    public void GiveAmmo(int amountToGive)
    {
        //same as GiveHealth, but for ammo
       weapon.curAmmo = Mathf.Clamp(weapon.curAmmo + amountToGive , 0, weapon.maxAmmo);
       GameUI.instance.UpdateAmmoText(weapon.curAmmo,weapon.maxAmmo); 
       
    }


}
