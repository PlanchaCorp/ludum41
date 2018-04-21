using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    float BALL_FORCE = 60f;
    float DISTANCE_MAX = 1f;

    GameObject ball;
    float mouseDownTime;
        
    void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }
	
	void Update () {
	}

    public void OnMouseDown()
    {
        mouseDownTime = Time.time;
    }

    public void OnMouseUp()
    {
        float elapsedTime = Time.time - mouseDownTime;
        ShootBall(Input.mousePosition, elapsedTime * 5);
    }

    private void ShootBall(Vector2 mousePosition, float elapsedTime)
    {
        // Calcul des distances
        float horizontalDistance = Camera.main.ScreenToWorldPoint(mousePosition).x - ball.transform.position.x;
        float verticalDistance = Camera.main.ScreenToWorldPoint(mousePosition).y - ball.transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(horizontalDistance, 2) + Mathf.Pow(verticalDistance, 2));
        // Application de la fonction sinus
        float horizontalForce = BALL_FORCE * horizontalDistance * (Mathf.Sin(elapsedTime) + 1);
        float verticalForce = BALL_FORCE * verticalDistance * (Mathf.Sin(elapsedTime + 3 * Mathf.PI/2) + 1);
        Debug.Log(horizontalForce);
        // Application des forces
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2(horizontalForce, verticalForce));
    }
}
