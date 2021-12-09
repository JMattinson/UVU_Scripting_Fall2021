using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused;
    public bool gameCont;
    public static GameManager instance;
    public int score;

    
    
    // Start is called before the first frame update
    void Start()
    {
        //setup, makes sure the game starts at score 0, unpaused, with the controls page open
        Time.timeScale = 1.0f;
        score = 0;
        gameCont = true;
        GameUI.instance.UpdateMissionText(score);
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player pressed either ESC or START on a gamepad
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
           
        }
        //check if the player pressed either BACKSPACE or SELECT on a gamepad
        if (Input.GetButtonDown("Submit")&&gamePaused)
        {
            GameUI.instance.OnMenuButton();
           
        }
        //Checks for either Left Bumper or TAB, to bring up the control overlay
                if (Input.GetButtonDown("Controls"))
        {
            ToggleContMenu();
           
        }

    }

    public void BreakGen()
    {
        //add to the total score, update the score in the UI
        score++;
        GameUI.instance.UpdateMissionText(score);
    }

    public void TogglePauseGame()
    {
        //freeze the game timer, if the game is paused
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        //toggle pause menu visual
        GameUI.instance.TogglePauseMenu(gamePaused);
    }

    public void ToggleContMenu()
    {
        //toggle if the control menu is considered open
        gameCont = !gameCont;
        //toggle Controls popup
        GameUI.instance.ToggleContMenu(gameCont); 
    }
    //boots the player into the game over screen when dead
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    //boots the player into the win screen if they enter the Exit, and have destroyed the generators
    public void WinGame()
    {
        //if all three generators are broken, player wins the game
        if(score == 3)
        SceneManager.LoadScene("Win");
    }
}
