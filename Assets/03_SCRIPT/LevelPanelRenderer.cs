using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPanelRenderer : MonoBehaviour {

    public LevelData LevelData;


 

    private void Start()
    {
        SetInfo();
    }

    // Use this for initialization
 
        public void SetInfo()
        {
            TextMeshProUGUI levelname = this.transform.Find("LevelName").GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI scoreName = this.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI parName = this.transform.Find("ParText").GetComponent<TextMeshProUGUI>();


            int playerScore = PlayerPrefs.GetInt(LevelData.sceneName);
            levelname.text = (LevelData.id) + ". " + LevelData.name;
            parName.text = LevelData.par + "";
            scoreName.text = playerScore + "";

            // Setting a silver or gold trophy if the player did succeed the level, and reached the par score
            this.transform.Find("Trophy").GetComponent<TrophyManager>().SetTrophyImage(playerScore, LevelData.par);

        }
    

    public void ChangeLevel()
    {
        SceneManager.LoadScene(LevelData.sceneName);
    }

  


	

}
