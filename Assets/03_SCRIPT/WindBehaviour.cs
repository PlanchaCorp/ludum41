using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehaviour : MonoBehaviour {
    /// <summary>
    /// Size of the wind effect
    /// </summary>
    [SerializeField]
    float effectArea = 5f;
    /// <summary>
    /// Strength of the wind 
    /// </summary>
    [SerializeField]
    public float windStrength = 10f;


	// Use this for initialization
	void Start () {
        GameObject wind = new GameObject("wind");
        wind.transform.parent = this.transform;
        wind.transform.localPosition = Vector3.zero;
        wind.transform.rotation = this.transform.rotation;
        wind.AddComponent<BoxCollider2D>();
        BoxCollider2D windBox = wind.GetComponent<BoxCollider2D>();

        windBox.size = new Vector2(this.GetComponent<BoxCollider2D>().size.x, effectArea);
        windBox.usedByEffector = true;
        windBox.isTrigger = true;
       
        windBox.offset = new Vector2(0, windBox.size.y /2);

        wind.AddComponent<AreaEffector2D>();
        AreaEffector2D windEffect = wind.GetComponent<AreaEffector2D>();
        windEffect.forceAngle = Quaternion.Inverse(transform.rotation).eulerAngles.z;


        GameObject windAnimation = GameObject.Find("windGfcs");
        windAnimation.transform.localScale = new Vector3(1, effectArea, 1);
        windAnimation.transform.localPosition = new Vector2(windAnimation.transform.localPosition.x, windAnimation.transform.localPosition.y + effectArea / 2);
        

        windEffect.forceMagnitude = windStrength;
  	
    }
	

}
