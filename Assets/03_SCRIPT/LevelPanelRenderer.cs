using System.Collections;
using System.Collections.Generic;
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
    public void SetInfo () {
        levelname.text = (levelId) + ". " + levelName;
        parName.text = levelPar + "";
        scoreName.text = PlayerPrefs.GetInt(levelSceneName) + "";
	}

    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelSceneName);
    }

  


	

}
