using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour {
    
    [SerializeField]
    private List<LevelData> levels;
    [SerializeField]
    private GameObject levelPanelPrefab;

    [SerializeField]
    private Button btnNext;

    [SerializeField]
    private Button btnPrev;


    public int offset = 0;
    public int size = 6;
    

    // Use this for initialization
    void Start () {

        LoadLevels();
    }
    public void NextPanel()
    {
        offset = offset +6;
        LoadLevels();
    }
    public void PreviousPanel()
    {
        offset = offset - 6;
        LoadLevels();
    }


    public void LoadLevels()
    {

        btnPrev.interactable = true;
        btnNext.interactable = true;

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < size; i++)
        {

            levelPanelPrefab.GetComponent<LevelPanelRenderer>().LevelData = levels[i+offset];
            //levelPanelPrefab.GetComponent<RectTransform>().localPosition = new Vector2((i % 3) * offsetX, 0);
            Instantiate(levelPanelPrefab, this.transform);
            levelPanelPrefab.name = levels[i+offset].name;

            //  SetInfo(levels[i]);
        }
        if(offset == 0)
        {
            btnPrev.interactable = false;
        }
        if(offset + size == levels.Count)
        {
            btnNext.interactable = false;
        }
    }
    

}
