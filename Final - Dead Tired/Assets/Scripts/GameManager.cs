using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused;
    public static GameManager instance;
    public int score;

    
    
    // Start is called before the first frame update
    void Start()
    {
        //sets the speed the game plays at, keeps the game from starting paused
        Time.timeScale = 1.0f;
        score = 0;
        
        GameUI.instance.UpdateMissionText(score);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
           
        }
        
        if (Input.GetButtonDown("Submit")&&gamePaused)
        {
            GameUI.instance.OnMenuButton();
           
        }
    }

    public void BreakGen()
    {
        score++;
        GameUI.instance.UpdateMissionText(score);
    }

    public void TogglePauseGame()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        //toggle pause menu visual
        GameUI.instance.TogglePauseMenu(gamePaused);
        

    }
    //boots the player into the game over screen when dead
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    //boots the player into the win screen if they enter the Exit, and have destroyed the generators
    public void WinGame()
    {
        if(score == 3)
        SceneManager.LoadScene("Win");
    }
}
