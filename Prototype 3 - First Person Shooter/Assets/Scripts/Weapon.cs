using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public GameObject bulletPrefab; (old code)
    
    public ObjectPool bulletPool; // (New code)

    public Transform muzzle;

    public int curAmmo;
    public int maxAmmo;

    public bool infAmmo;

    public float bulletSpeed;

    public float shootRate;

    public float lastShootTime;

    public bool isPlayer;

    //set up audio source and clips
    public AudioClip shootSFX;
    public AudioClip emptySFX;
    private AudioSource audioSource;

    

    void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked;
        if (GetComponent<playerController>())
        isPlayer = true;

        audioSource = GetComponent<AudioSource>();
    }

    public bool CanShoot() //determines if the weapon is ready to fire again
    {
           //checks to see how long it's been since the gun fired
           if(Time.time - lastShootTime >= shootRate)
           {
               //If weapon is ready, and has ammo, return true, can fire.
            if(curAmmo > 0 || infAmmo == true)
            return true;
           }
        
        return false;
    }

    public void Shoot()
    {
        //cooldown
        //makes the last time the gun fired right now.
        lastShootTime = Time.time; 
        curAmmo--;
        // create an instance of the bullet object, at the muzzle postition/rotation.
        // GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation); (old)
        
        GameObject bullet = bulletPool.GetObject(); // new code, using ObjectPool script

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        // add velocity to bullet.
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed; 
        if(isPlayer)
        {
            GameUI.instance.UpdateAmmoText(curAmmo,maxAmmo);
        }
        //play gunshot sound effect
        audioSource.PlayOneShot(shootSFX);
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
