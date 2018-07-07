using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuBeaviour : MonoBehaviour {

  

    public void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Replay()
    {
        this.GetComponentInParent<Canvas>().enabled  = false;
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void Pause()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().SetPause();
    }

    public void Unpause()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().Resume();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

   

}
