using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayindicator : MonoBehaviour {

    public GameObject ball;
    public GameObject hole;
    public GameObject image;
 

    // Update is called once per frame
    void Update () {
        //Debug.Log((ball.transform.position - hole.transform.position).x);

        image.transform.rotation = Quaternion.FromToRotation(Vector3.right ,hole.transform.position - ball.transform.position);
       
		
	}
}
