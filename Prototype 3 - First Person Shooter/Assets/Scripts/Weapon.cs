using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform muzzle;

    public int curAmmo;
    public int maxAmmo;

    public bool infAmmo;

    public float bulletSpeed;

    public float shootRate;

    public float lastShootTime;

    private bool isPlayer;

    

    void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked;
        if (GetComponent<playerController>())
        isPlayer = true;
    }

    public bool CanShoot() //determines if the weapon is ready to fire again
    {
           if(Time.time - lastShootTime >= shootRate)//checks to see how long it's been since the gun fired
           {
            if(curAmmo > 0 || infAmmo == true)
            return true;
           }

        return false;
    }

    public void Shoot()
    {
        //cooldown
        lastShootTime = Time.time; //makes the last time the gun fired right now.
        curAmmo--;
        // create an instance of the bullet object, at the muzzle postition/rotation.
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation); 
        // add velocity to bullet.
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
