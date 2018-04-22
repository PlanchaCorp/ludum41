﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPanelRenderer : MonoBehaviour {

    public LevelData LevelData;

    public TextMeshProUGUI levelname;
    public TextMeshProUGUI parName;
    public TextMeshProUGUI scoreName;

    // Use this for initialization
    public void SetInfo () {
        levelname.text = LevelData.id + ". " + LevelData.name;
        parName.text = LevelData.par + "";
        scoreName.text = LevelData.score + "";
	}


	

}
