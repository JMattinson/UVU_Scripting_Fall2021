using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExitTrigger : MonoBehaviour
{
    public GameManager GM;
  
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GM.WinGame();
        }
    }


}
