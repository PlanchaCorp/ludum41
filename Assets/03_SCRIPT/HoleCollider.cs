using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour {

    public Canvas canvas;

    void OnBecameInvisible()
    {
        Debug.Log("invisible");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Ball")
        {
            Victory();
        }
      
    }

    private void Victory()
    {
        Debug.Log("victory");
        canvas.enabled = true;
    }

}
