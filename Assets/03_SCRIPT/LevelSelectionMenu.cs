using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour {
    
    LevelPanelRenderer[] panels;
    public Sprite silverCup;
    public Sprite goldCup;


	// Use this for initialization
	void Start () {
        panels = gameObject.GetComponentsInChildren<LevelPanelRenderer>();// FindGameObjectsWithTag("levelSelector");
    
        for (int i =0; i < panels.Length; i++)
        {
            LevelPanelRenderer l = panels[i];
            l.SetInfo(silverCup, goldCup);
        }
	}
    public void NextPanel()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void LoadLevel()
    {

    }
}
