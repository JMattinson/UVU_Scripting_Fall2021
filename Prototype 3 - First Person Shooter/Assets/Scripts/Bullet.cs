using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifetime;
    private float shootTime;
    public GameObject hitParticle;

    void OnEnable()
    {
         shootTime = Time.time;


    }
    void OnTriggerEnter(Collider other)//on collision with an object
    {
        GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);//create particle effect
        Destroy(obj, 1.0f);

        if(other.CompareTag("Player")) //if target's player
            other.GetComponent<playerController>().TakeDamage(damage);//apply damage

        else if(other.CompareTag("Enemy"))//if target's enemy
            other.GetComponent<EnemyAI>().TakeDamage(damage);//apply damage
            
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
