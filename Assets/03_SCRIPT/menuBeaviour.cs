using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuBeaviour : MonoBehaviour
{
    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    public void Start()
    {
        RefreshMuteButton();
    }

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

    private void RefreshMuteButton()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        Image muteSprite = GameObject.FindGameObjectWithTag("MuteButton").GetComponent<Image>();
        if (muteSprite != null && music != null)
        {
            Sprite sprite = music.GetComponent<BackgroundMusic>().GetCurrentMuteSprite();
            muteSprite.sprite = sprite;
        }
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
        }
        else
        {
            muteSprite.sprite = mutedSprite;
        }
    }
}
