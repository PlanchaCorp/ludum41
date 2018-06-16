using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPanelRenderer : MonoBehaviour {

    public LevelData LevelData;
    public int levelId;
    public string levelName;
    public int levelPar;
    public string levelSceneName;

    public TextMeshProUGUI levelname;
    public TextMeshProUGUI parName;
    public TextMeshProUGUI scoreName;

    // Use this for initialization
    public void SetInfo (Sprite silverCup, Sprite goldCup) {
        int playerScore = PlayerPrefs.GetInt(levelSceneName);
        levelname.text = (levelId) + ". " + levelName;
        parName.text = levelPar + "";
        scoreName.text = playerScore + "";
        // Setting a silver or gold trophy if the player did succeed the level, and reached the par score
        if (playerScore != 0 && playerScore <= levelPar)
        {
            transform.Find("Trophy").gameObject.GetComponent<Image>().sprite = goldCup;
        } else if (playerScore != 0)
        {
            transform.Find("Trophy").gameObject.GetComponent<Image>().sprite = silverCup;
        }
        
	}

    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelSceneName);
    }

  


	

}
