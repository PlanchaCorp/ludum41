using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour {

    public List<LevelData> levels = new List<LevelData>();
    LevelPanelRenderer[] panels;


	// Use this for initialization
	void Start () {
        panels = gameObject.GetComponentsInChildren<LevelPanelRenderer>();// FindGameObjectsWithTag("levelSelector");
    
        for (int i =0; i < panels.Length; i++)
        {
            LevelPanelRenderer l = panels[i];
            if (levels[i] != null)
            {
                l.LevelData = levels[i];
                l.SetInfo();
            }
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
