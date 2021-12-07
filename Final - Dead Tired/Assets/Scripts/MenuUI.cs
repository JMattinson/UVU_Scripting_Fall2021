using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{
    //basic input checks, mainly for gamepad. 
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OnPlayButton();
        }
        
        if (Input.GetButtonDown("Submit"))
        {
            OnQuitButton();
           
        }
    }

    //When Play is pressed
    public void OnPlayButton()
    {
        //load the game
        SceneManager.LoadScene("test1");
    }
    //When quit is pressed
    public void OnQuitButton()
    {
        //close the application
        Application.Quit();
    }
}
