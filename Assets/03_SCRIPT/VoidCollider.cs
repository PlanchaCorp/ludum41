using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {
    GameObject ball;
    Vector3 ballInitialPosition;
    bool isDrowning = false;


    public void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballInitialPosition = ball.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball" && !isDrowning)
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(Rollback());
            isDrowning = true;
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.velocity = Vector3.zero;
            ballRb.gravityScale = 0.1f;
        }
    }

    private IEnumerator Rollback()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerControl>().GetLastStablePosition());
        ball.transform.position = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerControl>().GetLastStablePosition();
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.velocity = Vector3.zero;
        ballRb.gravityScale = 1;
        ballRb.angularVelocity = 0;
        isDrowning = false;
    }
}
