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
        RefreshMuteButton();
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
        } else
        {
            muteSprite.sprite = mutedSprite;
        }
    }
}
