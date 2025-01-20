using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class escMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

 void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    { if (Input.GetKeyDown(KeyCode.Escape))
        { 
       if (GameIsPaused)
    {
        Resum();
    } 
    else 
    {
        Pause();
    }
        
    }
    }
public void Resum () {
pauseMenuUI.SetActive(false);
Time.timeScale = 1f;
GameIsPaused = false;
}

void Pause(){
pauseMenuUI.SetActive(true);
Time.timeScale = 0f;
GameIsPaused = true;
}

public void LoadMenu(){
    Time.timeScale = 1f;
    SceneManager.LoadScene("Main Menu");
}
public void QuidGame(){
    Application.Quit();
}
    }




