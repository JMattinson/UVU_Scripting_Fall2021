using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused;
    public static GameManager instance;

    
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //sets the speed the game plays at, keeps the game from starting paused
        Time.timeScale = 1.0f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }
        
    }

    public void TogglePauseGame()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused == true ? 0.0f : 1.0f; 

    }
    //boots the player into the game over screen when dead
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
