using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenuUI : MonoBehaviour
{

    public Canvas CanvasUI;
    public GameObject MainMenuPanel;
    public GameObject SelectedLevelPanel;


    public void loadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void quit()
    {

        Application.Quit();
    }
    public void BtnPlay()
    {
        MainMenuPanel.SetActive(false);
        SelectedLevelPanel.SetActive(true);
    }
    public void BtnBack()
    {
        MainMenuPanel.SetActive(true);
        SelectedLevelPanel.SetActive(false);
    }
}
