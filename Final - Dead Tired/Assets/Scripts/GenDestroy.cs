using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenDestroy : MonoBehaviour
{
     public GameManager GM;
  
    void OnTriggerEnter(Collider other)
    {
        //if the generator is shot at, updates the score in game manager, and destroys self
        if(other.CompareTag("Bullet"))
        {
            GM.BreakGen();
            gameObject.SetActive(false);
        }
    }
}
