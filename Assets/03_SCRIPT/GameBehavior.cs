using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehavior : MonoBehaviour {
    private int mapScore;
    public  int mapPar = 0;
    public int levelNumber = 0;
    public static Dictionary<int,int> scores = new Dictionary<int,int>();
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
        switch ( mapScore - mapPar)
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
            text.text = mapPar + "";
        }
        
    }

    public void UpdateTotal()
    {
        GameObject[] totalTexts = GameObject.FindGameObjectsWithTag("TotalText");
        TextMeshProUGUI holeText = GameObject.Find("holeNum").GetComponent<TextMeshProUGUI>(); ;
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
