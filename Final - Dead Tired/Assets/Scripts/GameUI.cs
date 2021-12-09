using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI missionText;

    [Header ("Ingame Menus")]
    public GameObject pauseMenu;
    public GameObject contMenu;


    // Instance/Singleton
    public static GameUI instance;

    void Awake()
    {
        //set instance to this script
        instance = this;
    }

    //assigns a Text status to a given health level
    public void UpdateHealthBar(int curHP)
    {
        
        if(curHP>=8)
            healthText.text = "Status: Good";
        else if(curHP<8 && curHP >2)
            healthText.text = "Status: Injured";
        else
            healthText.text = "Status: DANGER";
        
    }

    //keeps track of the ammo count in the UI
    public void UpdateAmmoText(int curAmmo, int maxAmmo)
    {
        ammoText.text = "Ammo : " + curAmmo + "/" + maxAmmo;
    }
    //updates the player's mission in the UI
    public void UpdateMissionText(int score)
    {
        if(score<3)
        missionText.text = ("Break the Generators and escape! Generators broken: " + score) +"/3";
        else
        missionText.text = ("You've broken them all, run back to the escape portal!");
    }

    // toggles the pause menu visual
    public void TogglePauseMenu(bool paused)
    {
        
        pauseMenu.SetActive(paused);
    }

    //toggles the control menu visual
    public void ToggleContMenu(bool open)
    {
        contMenu.SetActive(open);
    }


    //resumes the game
    public void OnResumeButton()
    {
        GameManager.instance.TogglePauseGame();
    }

    //returns to main menu
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
