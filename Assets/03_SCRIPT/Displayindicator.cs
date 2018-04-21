using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displayindicator : MonoBehaviour {

    public GameObject ball;
    public GameObject hole;
    int screenHeight;
    int screenWidh;

    private void Start()
    {
        screenWidh = Screen.width;
        //Debug.Log(screenWidh);
    }
    // Update is called once per frame
    void Update () {
        //Debug.Log((ball.transform.position - hole.transform.position).x);

        /*if ((ball.transform.position - hole.transform.position).x > screenWidh / 2)
        {
            Debug.Log("hide");
        } else
        {
            Debug.Log("show");
        }*/
		
	}
}
