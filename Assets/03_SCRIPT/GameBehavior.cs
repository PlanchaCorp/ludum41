using TMPro;
using UnityEngine;

public class GameBehavior : MonoBehaviour {
    private int mapScore;

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
    
	void Start () {
        ResetScore();
	}

    private void UpdateScore()
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreText");
        foreach (GameObject scoreText in scoreTexts)
        {
            TextMeshProUGUI text = scoreText.GetComponent<TextMeshProUGUI>();
            text.text = mapScore + "";
        }
    }
}
