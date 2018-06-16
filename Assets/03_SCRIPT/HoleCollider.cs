using UnityEngine;


/// <summary>
/// Victory hole behavior
/// </summary>
public class HoleCollider : MonoBehaviour {
    /// <summary>
    /// Victory canvas
    /// </summary>
    public Canvas canvas;

  
    /// <summary>
    /// Trigerring victory when ball enters hole
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            Time.timeScale = 0;
             Victory();
        }
    }

    /// <summary>
    /// Triggers victory
    /// </summary>
    private void Victory()
    {
        canvas.enabled = true;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().SetLevelScore();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().UpdateTotal();
    }

}
