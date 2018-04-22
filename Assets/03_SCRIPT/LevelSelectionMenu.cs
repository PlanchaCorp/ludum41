using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour {

    public List<LevelData> levels = new List<LevelData>();
    GameObject[] panels;


	// Use this for initialization
	void Start () {
        panels = GameObject.FindGameObjectsWithTag("levelSelector");
        Debug.Log(panels.Length);
        foreach ( LevelData level in levels)
        {
            LevelPanelRenderer l =  panels[level.id].GetComponent<LevelPanelRenderer>();
            
            l.LevelData = level;
            l.SetInfo();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadLevel()
    {

    }
}
