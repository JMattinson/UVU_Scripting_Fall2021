using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public string pickupName;
    public int amount;

    public GameManager gameManager ;

    void Update()
    {

        transform.Rotate(Vector3.back * 50 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
       {
        print ("You picked up a key!");
        gameManager.hasKey = true;
        Destroy(gameObject);
       }
    }
}
