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

    private float lastQTurn;
    public float qTurnRate;

    [Header("Health")]
    public int curHP;
    public int maxHP;

     private Weapon weapon;
    
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
    }
    public void Die()
    {
        //placeholder, add an actual deathstate
        print("Dead.");
    }
    void FixedUpdate()
    {
        MoveMod();
        PlayerMove();
         if(AimingCheck() && Input.GetKey(KeyCode.J))//If I'm aiming & shooting
        {
            if(weapon.CanShoot())//fire my weapon
                weapon.Shoot();
        }
       
    }

    void PlayerMove () //Manages player movement proper, is modified by MoveMod()
    {
        // movement, left/right
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, turnSpeed* hInput * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
        
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
        {speed = 1.5f; }
        //error handler
        else speed = 2.0f;

        //is the player trying o quickturn?
        if(QuickTurn())
        {
            //pull a fast 180
            transform.Rotate(Vector3.up,-180);   
        }
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

    public bool QuickTurn()
    {
        //this basic timer keeps the player from SPEEN, also checks for qturn button
        if (Input.GetKey(KeyCode.X) && Time.time - lastQTurn >= qTurnRate)
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
       curHP = Mathf.Clamp(curHP + amountToGive , 0, maxHP); 

    }
    public void GiveAmmo(int amountToGive)
    {
       weapon.curAmmo = Mathf.Clamp(weapon.curAmmo + amountToGive , 0, weapon.maxAmmo); 
       
    }


}
