using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehavior : MonoBehaviour {
    private int mapScore;
    public  int mapPar = 0;
    public static List<int> scores = new List<int>();
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

    public void SetLevelScore(int score)
    {
        scores.Add(score);
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

        }
        foreach (GameObject scoreText in scoreTexts)
        {
            Debug.Log(scoreText);
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
        UpdateTotal();
    }

    public void UpdateTotal()
    {
        GameObject[] totalTexts = GameObject.FindGameObjectsWithTag("TotalText");
        foreach (GameObject parText in totalTexts)
        {
            TextMeshProUGUI text = parText.GetComponent<TextMeshProUGUI>();
            text.text = "";
            int sum = 0;
            foreach (int score in scores)
            {
                sum += score;
                text.text += scores + "  ";
            }
            text.text += sum;



        }
    }


}
