using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//Dies when touched by projectile
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Projectile"))
         Destroy(gameObject);
        
        
    }
}
