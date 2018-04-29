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

    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    public void Start()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }
    }


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

    public void ToggleMute()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        Image muteSprite = GameObject.FindGameObjectWithTag("MuteButton").GetComponent<Image>();
        if (muteSprite == null || music == null) return;
        bool musicPlays = music.GetComponent<BackgroundMusic>().ToggleMusic();
        if (musicPlays)
        {
            muteSprite.sprite = unmutedSprite;
        } else
        {
            muteSprite.sprite = mutedSprite;
        }
    }
}
