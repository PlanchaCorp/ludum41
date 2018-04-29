using System.Collections;
using UnityEngine;


/// <summary>
/// Water behavior
/// </summary>
public class VoidCollider : MonoBehaviour {
    /// <summary>
    /// Variable referencing the ball
    /// </summary>
    private GameObject ball;
    /// <summary>
    /// Boolean true when the ball is in the water
    /// </summary>
    private bool isDrowning = false;


    /// <summary>
    /// Initialize the ball reference and its position
    /// </summary>
    public void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    /// <summary>
    /// Action when the ball just reached the water
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Teleports the ball to its last stable position
    /// </summary>
    /// <returns></returns>
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
