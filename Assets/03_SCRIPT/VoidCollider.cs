using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {
    GameObject ball;
    Vector3 ballInitialPosition;


    public void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballInitialPosition = ball.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            Defeat();
        }
    }

    private void Defeat()
    {
        Debug.Log("defeat");
        ball.transform.position = ballInitialPosition;
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = 0;
        gameObject.GetComponent<GameBehavior>().ResetScore();
    }
}
