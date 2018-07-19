using UnityEngine.UI;
using UnityEngine;
using System.Collections;


/// <summary>
/// Managing the player controls with ball forces and character teleportation
/// </summary>
public class PlayerControl : MonoBehaviour {
    /// <summary>
    /// Power of the ball
    /// </summary>
    public float BALL_FORCE = 400f;
    /// <summary>
    /// Prefab of the arrow to spawn
    /// </summary>
    public GameObject arrowPrefab;
    /// <summary>
    /// Prefab of the player teleporting warp to spawn
    /// </summary>
    public GameObject warpPrefab;


    /// <summary>
    /// Variable referencing the ball
    /// </summary>
    private GameObject ball;
    /// <summary>
    /// Variable referencing the character
    /// </summary>
    private GameObject character;
    /// <summary>
    /// Variable referencing the current shoot arrow
    /// </summary>
    private GameObject currentArrow;
    /// <summary>
    /// Variable memorizing the warp used before the player teleports (if exists)
    /// </summary>
    private GameObject enterWarp = null;
    /// <summary>
    /// Variable memorizing the warp used after the player teleports (if exists)
    /// </summary>
    private GameObject exitWarp = null;


    /// <summary>
    /// Last remembered position of the ball when still
    /// </summary>
    private Vector2 lastStablePosition;
    /// <summary>
    /// Time elapsed with mouse down
    /// </summary>
    private float mouseDownTime = 0f;
    /// <summary>
    /// Time elapsed since holding shoot button
    /// </summary>
    private float shootingTime = 0f;
    /// <summary>
    /// Boolean true when character is teleporting
    /// </summary>
    private bool isTeleporting = false;
    /// <summary>
    /// Boolean true when character is in shooting phase, before he hits the ball
    /// </summary>
    private bool isShooting = false;
        

    /// <summary>
    /// Save references to ball and character gameobjects at startup
    /// </summary>
    public void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
        character = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Update phase
    /// </summary>
    public void Update() {
        // Reposition camera and background
        GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = Quaternion.identity;
        GameObject.FindGameObjectWithTag("Background").transform.rotation = Quaternion.identity;
        // Position and fill shoot arrow
        if (mouseDownTime > 0)
        {
            shootingTime = (Time.time - mouseDownTime) * 7;
            PositionArrow(Input.mousePosition, shootingTime);
        }
        // Handle actions if actions are available
        if (!PlayerIsMoving() && !isTeleporting && !GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().IsPaused() &&
            !GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().IsDialogPrinted())
        {
            StartCoroutine(HandleActions());
        }
    }

    /// <summary>
    /// Handling different play actions
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleActions()
    {
        yield return new WaitForSeconds(0.4f);
        bool playerIsMoving = PlayerIsMoving();
        // Left click pressed
        if (Input.GetMouseButtonDown(0) && !playerIsMoving)
        {
            mouseDownTime = Time.time;
            if (currentArrow != null)
            {
                Destroy(currentArrow);
            }
            currentArrow = GameObject.Instantiate(arrowPrefab, ball.transform.position, Quaternion.identity);
            currentArrow.transform.parent = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            currentArrow.transform.localScale = new Vector2(currentArrow.transform.localScale.x / 10, currentArrow.transform.localScale.y / 10);
        }
        // Left click released
        if (Input.GetMouseButtonUp(0) && mouseDownTime > 0 && !playerIsMoving)
        {
            Destroy(currentArrow);
            mouseDownTime = 0;
         
            StartCoroutine(ShootBall(Input.mousePosition, shootingTime));
        }
        // Teleportation
        if (!playerIsMoving && ball != null)
        {
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.velocity = Vector3.zero;
            StartCoroutine(TeleportPlayerIfStill());
            lastStablePosition = ball.transform.position;
        }
    }

    /// <summary>
    /// Shoot the ball
    /// </summary>
    /// <param name="mousePosition">Current position of the mouse</param>
    /// <param name="elapsedTime">Time elapsed since holding the mouse button</param>
    /// <returns></returns>
    private IEnumerator ShootBall(Vector3 mousePosition, float elapsedTime)
    {
        isShooting = true;
        // Sound effect and animation
        ball.GetComponent<AudioSource>().Play();
        character.GetComponent<Animator>().SetTrigger("Shoot");
        // Distances calculation
        mousePosition.z = 1;
        float horizontalDistance = Camera.main.ScreenToWorldPoint(mousePosition).x - ball.transform.position.x;
        float verticalDistance = Camera.main.ScreenToWorldPoint(mousePosition).y - ball.transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(horizontalDistance, 2) + Mathf.Pow(verticalDistance, 2));
        // Sinus function application
        float horizontalForce = BALL_FORCE * (horizontalDistance/distance) * (Mathf.Sin(elapsedTime + 3 * Mathf.PI / 2) + 1);
        float verticalForce = BALL_FORCE * (verticalDistance/distance) * (Mathf.Sin(elapsedTime + 3 * Mathf.PI/2) + 1);
        // Waiting for the animation to reach the hit sequence
        yield return new WaitForSeconds(0.2f);
        // Applying forces
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2(horizontalForce, verticalForce));
        // Increment score
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().IncrementScore();
        isShooting = false;
    }

    /// <summary>
    /// Position and fill the shooting arrow
    /// </summary>
    /// <param name="mousePosition">Current mouse position</param>
    /// <param name="elapsedTime">Time elapsed since holding the mouse button</param>
    private void PositionArrow(Vector3 mousePosition, float elapsedTime)
    {
        if (currentArrow == null)
        {
            return;
        }
        // Position and rotate the arrow
        mousePosition.z = 1;
        Vector2 localMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        currentArrow.transform.rotation = Quaternion.FromToRotation(Vector3.right, localMousePosition - new Vector2(ball.transform.position.x, ball.transform.position.y));
        currentArrow.transform.position = ball.transform.position + currentArrow.transform.rotation * (new Vector2(0.5f, 0f));
        // Fill up the power bar
        GameObject[] arrowFillers = GameObject.FindGameObjectsWithTag("ArrowFiller");
        foreach(GameObject arrowFiller in arrowFillers)
        {
            Image fillerImage = arrowFiller.GetComponent<Image>();
            if (fillerImage != null)
            {
                fillerImage.fillAmount = (Mathf.Sin(elapsedTime + 3 * Mathf.PI / 2) + 1) / 2f;
            }
        }
        // Rotate the character
        RotatePlayer(currentArrow.transform.eulerAngles.z);
    }

    /// <summary>
    /// Attests that the player is moving or not
    /// </summary>
    /// <returns>Boolean true if the player is moving or shooting</returns>
    private bool PlayerIsMoving()
    {
        if (ball != null)
        {
            Vector2 ballVelocity = ball.GetComponent<Rigidbody2D>().velocity;
            float ballForce = Mathf.Sqrt(Mathf.Pow(ballVelocity.x, 2) + Mathf.Pow(ballVelocity.y, 2));
            return isShooting || ballForce >= 0.2f;
        } else
        {
            Debug.Log("PlayerIsMoving : ball = null !");
            return false;
        }
    }

    /// <summary>
    /// Rotate the player left or right
    /// </summary>
    /// <param name="arrowRotation">Current rotation (in degree) of the shooting arrow (starting right)</param>
    private void RotatePlayer(float arrowRotation)
    {
        if (character != null)
        {
            character.transform.eulerAngles = new Vector3(0.0f, (arrowRotation > 90 && arrowRotation <= 270) ? 180 : 0, 0);
        } else
        {
            Debug.Log("RotatePlayer : character = null !");
        }
    }

    /// <summary>
    /// Triggers teleportation if player is still
    /// </summary>
    /// <returns></returns>
    private IEnumerator TeleportPlayerIfStill()
    {
        yield return new WaitForSeconds(0.2f);
        if (!PlayerIsMoving())
        {
            if (character != null && ball != null)
            {
                Vector3 newCharacterPosition = ball.transform.position + new Vector3(0, -ball.GetComponent<CircleCollider2D>().bounds.size.y / 2);
                float distance = Mathf.Sqrt(Mathf.Pow(character.transform.position.x - newCharacterPosition.x, 2) + Mathf.Pow(character.transform.position.y - newCharacterPosition.y, 2));
                if (character.transform.position != newCharacterPosition && exitWarp == null && distance >= 0.5f)
                {
                    StartCoroutine(TeleportAndAnimate(newCharacterPosition));
                }
            }
            else
            {
                Debug.Log("TeleportPlayer : character || ball = null !");
            }
        }
    }

    /// <summary>
    /// Effectively teleports and animates the character
    /// </summary>
    /// <param name="newPosition">New position to teleport the character to</param>
    /// <returns></returns>
    private IEnumerator TeleportAndAnimate(Vector3 newPosition)
    {
        if (warpPrefab != null)
        {
            isTeleporting = true;
            exitWarp = GameObject.Instantiate(warpPrefab, ball.transform.position, Quaternion.identity);
            enterWarp = GameObject.Instantiate(warpPrefab, character.transform.position, Quaternion.identity);
            StartCoroutine(DestroyAnimation());
        } else
        {
            Debug.Log("AnimateTeleportation : warp = null !");
        }
        yield return new WaitForSeconds(0.45f);
        character.transform.position = newPosition;
    }

    /// <summary>
    /// Destroys teleportation animations
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(exitWarp);
        Destroy(enterWarp);
        isTeleporting = false;
    }

    /// <summary>
    /// Getter for the last player stable position
    /// </summary>
    /// <returns>Vector of the last stable position of the ball</returns>
    public Vector2 GetLastStablePosition()
    {
        return lastStablePosition;
    }
}
