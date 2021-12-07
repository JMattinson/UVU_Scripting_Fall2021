using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset =new Vector3(0,4,-14);
   

    // Update is called once per frame
    void Update()
    {
        //focus in on the player's location, follow them
        transform.position = player.transform.position + offset;
    }
}
