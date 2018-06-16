using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    public CircleCollider2D portalCenter;

    private CircleCollider2D attractField;

    private GameObject ball;

    private bool inbound = false;
    /// <summary>
    /// Radius of the attract field
    /// </summary>
    public float radius = 3;

    /// <summary>
    /// The force of the field. If it's less than 0, it would be attractive else if it's greater than 0 it would be repulsive
    /// </summary>
    public float magnitude = 0;

    /// <summary>
    /// Portal sprite
    /// </summary>
    private SpriteRenderer portalRenderer;

    /// <summary>
    /// 
    /// </summary>
    private PointEffector2D attraction;



    private void Start()
    {
       // portalCenter = GetComponentInChildren<CircleCollider2D>();

        attractField = GetComponent<CircleCollider2D>();

        attractField.radius = radius;

       

        attraction = GetComponent<PointEffector2D>();
        attraction.forceMagnitude = magnitude;
        
        portalRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (magnitude < 0)
        {
            portalRenderer.color = new Color(255, 0, 0);
        }
        else
        {
            portalRenderer.color = new Color(0, 0, 255);
        }

    }
 

    private void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.tag  == "Ball")
        {
            ball = other.gameObject;
        } 

    }



    private void OnTriggerExit2D(Collider2D other)
    {
        ball = null;
    }

    private void Update()
    {
        if ( ball != null)
        {
           Rigidbody2D rigidbody =  ball.GetComponent<Rigidbody2D>();
            Vector2 speed = rigidbody.velocity;
            bool touchCenter = rigidbody.IsTouching(portalCenter);
            if(touchCenter == true && (speed.x <5 && speed.y < 5))
            {
               
                   Vector3 lastPos = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerControl>().GetLastStablePosition();
                    Rollback(lastPos);
               
                
              // rigidbody.velocity = Vector2.zero;
               //ball.transform.localScale -=  new Vector3(0.99f, 0.99f, 1f);
                
            }
        }
    }

    /// <summary>
    /// /!\ code duplicated from voidcollider
    /// Teleports the ball to its last stable position
    /// </summary>
    /// <returns></returns>
    private void Rollback(Vector3 lastPos)
    {      
        ball.transform.position = lastPos;
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.velocity = Vector3.zero;
        ballRb.gravityScale = 1;
        ballRb.angularVelocity = 0;
      
       
    }
}
