using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehaviour : MonoBehaviour {
    /// <summary>
    /// Size of the wind effect
    /// </summary>
    public float effectArea = 5f;
    /// <summary>
    /// Strength of the wind 
    /// </summary>
    public float windStrength = 10f;


	// Use this for initialization
	void Start () {

        BoxCollider2D windBox = gameObject.GetComponent<BoxCollider2D>();

        windBox.size = new Vector2(windBox.size.x ,effectArea);
        windBox.offset = new Vector2(0, windBox.size.y /2);
        AreaEffector2D windEffect = gameObject.GetComponent<AreaEffector2D>();

        GameObject windAnimation = GameObject.Find("wind_sprite");
        windAnimation.transform.localScale = new Vector3(1, effectArea, 1);
        windAnimation.transform.position = new Vector2(windAnimation.transform.position.x, windAnimation.transform.position.y + effectArea / 2);
        

        windEffect.forceMagnitude = windStrength;
  	
    }
	

}
