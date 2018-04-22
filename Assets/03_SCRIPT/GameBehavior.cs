using TMPro;
using UnityEngine;

public class GameBehavior : MonoBehaviour {
    private int mapScore;
    public  int mapPar = 0;
    public static int[] scores = new int[3];
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

    public void SetLevelScore(int level, int score)
    {
        scores[level] = score;
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
    }


}
