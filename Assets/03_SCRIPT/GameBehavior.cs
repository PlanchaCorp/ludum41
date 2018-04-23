using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour {
    private int mapScore;
    public LevelData levelData;
    public static Dictionary<int,int> scores = new Dictionary<int,int>();

    TextMeshProUGUI dialogue;
    TextMeshProUGUI title;
    Image dialogueFrame;

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
        } else
        {
            scores.Add(levelData.id, mapScore);
        }

        string activeSceneName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.GetInt(activeSceneName) > mapScore)
        {
            PlayerPrefs.SetInt(activeSceneName, mapScore);
        }
    }


    
	void Start () {
        ResetScore();


        gameObject.GetComponent<GameBehavior>().UpdatePar();

         title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        title.text = (levelData.id +1 )+ ". " + levelData.name;


         dialogue = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();

        dialogueFrame = GameObject.Find("ImageDialogue").GetComponent<Image>();

        dialogue.text = levelData.dialogue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetMouseButtonDown(0)) { 
            dialogue.enabled = false;
            title.enabled = false;
            dialogueFrame.enabled = false;
       
        }
    }

    private void UpdateScore()
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreText");
        TextMeshProUGUI scoreCommentText = GameObject.Find("ScoreComment").GetComponent<TextMeshProUGUI>();
        switch (levelData.par - mapScore)
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

    public void UpdatePar()
    {
        GameObject[] parTexts = GameObject.FindGameObjectsWithTag("ParText");
        foreach (GameObject parText in parTexts)
        {
            TextMeshProUGUI text = parText.GetComponent<TextMeshProUGUI>();
            text.text = levelData.par + "";
        }
        
    }

    public void UpdateTotal()
    {
        GameObject[] totalTexts = GameObject.FindGameObjectsWithTag("TotalText");
        TextMeshProUGUI holeText = GameObject.Find("holeNum").GetComponent<TextMeshProUGUI>(); 
        foreach (GameObject parText in totalTexts)
        {
            TextMeshProUGUI text = parText.GetComponent<TextMeshProUGUI>();
            text.text = "";
            int sum = 0;
            foreach (KeyValuePair<int,int> score in scores)
            {
               
                sum += score.Value;
                holeText.text += score.Key + "  ";
                text.text += score.Value + "  ";
            }
            text.text += sum;
            holeText.text += "T";



        }
    }


}
