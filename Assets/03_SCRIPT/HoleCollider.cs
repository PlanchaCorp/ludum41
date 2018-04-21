using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour {


     void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log(other.tag);
    }


}
