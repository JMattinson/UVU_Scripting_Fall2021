using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifetime;
    private float shootTime;

    public GameObject hitParticle;
    public float particleTimer;

    void OnEnable()
    {
        shootTime = Time.time;



    }
    void OnTriggerEnter(Collider other)//on collision with an object
    {
        //Creates the particle effect on contact, set particle death tiemr
        GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);//create particle effect
        Destroy(obj, particleTimer);

        if(other.CompareTag("Player")) //if target's player
            other.GetComponent<PlayerController>().TakeDamage(damage);//apply damage

        else if(other.CompareTag("Enemy"))//if target's enemy
            other.GetComponent<EnAI>().TakeDamage(damage);//apply damage
            
        gameObject.SetActive(false);//disable self
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);
    }

}
