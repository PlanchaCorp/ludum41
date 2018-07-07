using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    private int mapScore;
    public LevelData levelData;
   /* public int levelId;
    public string levelName;
    public string levelDialog;
    public int levelPar;*/
    static private bool isPaused = false;

    public static Dictionary<int, int> scores = new Dictionary<int, int>();


    TextMeshProUGUI dialogue;
    TextMeshProUGUI title;
    Image dialogueFrame;
    Canvas pauseCanvas;

    public void ResetScore()
    {
        mapScore = 0;
        UpdateScore();
    }
    public void IncrementScore()
    {
        mapScore++;
        UpdateScore();
    }

    public int GetScore()
    {
        return mapScore;
    }

    public void SetLevelScore()
    {
        if (scores.ContainsKey(levelData.id))
        {
            scores[levelData.id] = mapScore;
        }
        else
        {
            scores.Add(levelData.id, mapScore);
        }

        string activeSceneName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.GetInt(activeSceneName) > mapScore || PlayerPrefs.GetInt(activeSceneName) == 0)
        {
            PlayerPrefs.SetInt(activeSceneName, mapScore);
        }
    }



    void Start()
    {
        ResetScore();


        Time.timeScale = 1;
        gameObject.GetComponent<GameBehavior>().UpdatePar();
        title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        title.text = (levelData.id) + ". " + levelData.name;

        dialogue = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        dialogueFrame = GameObject.Find("ImageDialogue").GetComponent<Image>();

        dialogue.text = levelData.dialogue;


        pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                SetPause();
            } else
            {
                Resume();
            }
        }
        if (Input.GetMouseButtonDown(0) && !isPaused)
        {
            dialogue.enabled = false;
            title.enabled = false;
            dialogueFrame.enabled = false;

        }
    }

    public void SetPause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void UpdateScore()
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreText");
        TextMeshProUGUI scoreCommentText = GameObject.Find("ScoreComment").GetComponent<TextMeshProUGUI>();
        switch (mapScore - levelData.par)
        {
            case (2):
                scoreCommentText.text = "Double Bogey";
                break;
            case (1):
                scoreCommentText.text = "Bogey";
                break;
            case (0):
                scoreCommentText.text = "Par";
                break;
            case (-1):
                scoreCommentText.text = "Birdy";
                break;
            case (-2):
                scoreCommentText.text = "Eagle";
                break;
            case (-3):
                scoreCommentText.text = "Albatros";
                break;
            default:
                scoreCommentText.text = "Finish";
                break;

        }
        if (mapScore == 1)
        {
            scoreCommentText.text = "Hole in one";
        }
        foreach (GameObject scoreText in scoreTexts)
        {

            TextMeshProUGUI text = scoreText.GetComponent<TextMeshProUGUI>();
            text.text = mapScore + "";
        }
    }

    public bool IsDialogPrinted()
    {
        return dialogue.enabled;
    }

    public void UpdatePar()
    {
        GameObject[] parTexts = GameObject.FindGameObjectsWithTag("ParText");
        foreach (GameObject parText in parTexts)
        {
            TextMeshProUGUI text = parText.GetComponent<TextMeshProUGUI>();
            text.text = levelData.par + "";
        }

    }




}
