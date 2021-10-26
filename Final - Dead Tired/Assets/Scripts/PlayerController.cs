using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float turnSpeed;
    public float hInput;
    public float vInput;

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

        transform.Rotate(Vector3.up, turnSpeed* hInput * Time.deltaTime );
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
        
    }

    void MoveMod () //Checks if anything's changing player speed, Using AimingCheck() & RunCheck()
    {
        if (RunCheck() && !AimingCheck() && vInput > 0) 
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
