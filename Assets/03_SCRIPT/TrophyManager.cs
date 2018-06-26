using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyManager : MonoBehaviour {
    [SerializeField]
    private Sprite emptyTrophy;

    [SerializeField]
    private Sprite silverTrophy;

    [SerializeField]
    private Sprite goldTrophy;

    
    public void SetTrophyImage(int levelScore, int levelPar)
    {
        if (levelScore != 0 && levelScore <= levelPar)
        {
            this.GetComponent<Image>().sprite = goldTrophy;
        }
        else if (levelScore != 0)
        {
            this.GetComponent<Image>().sprite = silverTrophy;
        }
        else
        {
            this.GetComponent<Image>().sprite = emptyTrophy;
        }
    }
	
}
