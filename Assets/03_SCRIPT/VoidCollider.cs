using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {

    public Canvas canvas;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        Defeat();
    }

    private void Defeat()
    {
        Debug.Log("defeat");
        canvas.enabled = true;
    }
}
