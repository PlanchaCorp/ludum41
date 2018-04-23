using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehavior : MonoBehaviour {
    private int mapScore;
    public LevelData levelData;
    public int mapPar;
    public int levelNumber;
    public static Dictionary<int,int> scores = new Dictionary<int,int>();
    public void ResetScore()
    {
        levelData.score = 0;
        UpdateScore();
    }
    public void IncrementScore()
    {
        levelData.score++;
        UpdateScore();
    }

    public int GetScore()
    {
        return levelData.score;
    }

    public void SetLevelScore()
    {
        if (scores.ContainsKey(levelNumber))
        {
            scores[levelNumber] = mapScore;
        } else
        {
            scores.Add(levelNumber, mapScore);
        }
      
    }


    
	void Start () {
        ResetScore();

        gameObject.GetComponent<GameBehavior>().UpdatePar();
    }

    private void UpdateScore()
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreText");
        TextMeshProUGUI scoreCommentText = GameObject.Find("ScoreComment").GetComponent<TextMeshProUGUI>();
        switch ( levelData.score - levelData.par)
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
        if (levelData.score == 1)
        {
            scoreCommentText.text = "Hole in one";
        }
        foreach (GameObject scoreText in scoreTexts)
        {
            
            TextMeshProUGUI text = scoreText.GetComponent<TextMeshProUGUI>();
            text.text = levelData.score + "";

            
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
            Debug.Log(sum);
            text.text += sum;
            holeText.text += "T";



        }
    }


}
