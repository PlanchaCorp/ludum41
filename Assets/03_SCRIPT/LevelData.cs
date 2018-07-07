using UnityEngine;

[CreateAssetMenu(fileName = "New Level ", menuName= "levelData")]
public class LevelData : ScriptableObject 
{
    public int id;
    public new string name;
    public string sceneName;
    public int par;
    public int score;
    [TextArea]
    public string dialogue;
}