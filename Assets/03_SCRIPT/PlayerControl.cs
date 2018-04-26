using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float BALL_FORCE = 400f;
    public float DISTANCE_MAX = 1f;

    public GameObject arrowPrefab;
    public GameObject warpPrefab;

    GameObject ball;
    GameObject character;
    float mouseDownTime = 0f;
    float shootingTime = 0f;
    private GameObject currentArrow;
    private int collisionCount = 0;
    private Vector2 lastStablePosition;
    private GameObject enterWarp = null;
    private GameObject exitWarp = null;
    private bool isTeleporting = false;
        
    void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
        character = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        // Positionnement de la camera et du background
        GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = Quaternion.identity;
        GameObject.FindGameObjectWithTag("Background").transform.rotation = Quaternion.identity;
        // Positionnement de la flèche de tir
        if (mouseDownTime > 0)
        {
            shootingTime = (Time.time - mouseDownTime) * 10;
            PositionArrow(Input.mousePosition, shootingTime);
        }
        if (!PlayerIsMoving() && !isTeleporting && !GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().IsDialogPrinted())
        {
            StartCoroutine(HandleActions());
        }
    }

    private IEnumerator HandleActions()
    {
        yield return new WaitForSeconds(0.4f);
        bool playerIsMoving = PlayerIsMoving();
        // Clic gauche appuyé
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
        // Clic gauche relaché
        if (Input.GetMouseButtonUp(0) && mouseDownTime > 0 && !playerIsMoving)
        {
            Destroy(currentArrow);
            mouseDownTime = 0;
            Debug.Log("Shot");
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

    private IEnumerator ShootBall(Vector3 mousePosition, float elapsedTime)
    {
        // Effet sonore et animation
        ball.GetComponent<AudioSource>().Play();
        character.GetComponent<Animator>().SetTrigger("Shoot");
        // Calcul des distances
        mousePosition.z = 1;
        float horizontalDistance = Camera.main.ScreenToWorldPoint(mousePosition).x - ball.transform.position.x;
        float verticalDistance = Camera.main.ScreenToWorldPoint(mousePosition).y - ball.transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(horizontalDistance, 2) + Mathf.Pow(verticalDistance, 2));
        // Application de la fonction sinus
        float horizontalForce = BALL_FORCE * (horizontalDistance/distance) * (Mathf.Sin(elapsedTime + 3 * Mathf.PI / 2) + 1);
        float verticalForce = BALL_FORCE * (verticalDistance/distance) * (Mathf.Sin(elapsedTime + 3 * Mathf.PI/2) + 1);
        // Attente de l'animation
        yield return new WaitForSeconds(0.2f);
        // Application des forces
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2(horizontalForce, verticalForce));
        // Incrémentation du score  
      
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehavior>().IncrementScore();
    }

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

    private bool PlayerIsMoving()
    {
        if (ball != null)
        {
            Vector2 ballVelocity = ball.GetComponent<Rigidbody2D>().velocity;
            float ballForce = Mathf.Sqrt(Mathf.Pow(ballVelocity.x, 2) + Mathf.Pow(ballVelocity.y, 2));
            /*RaycastHit2D[] verticalRaycastHits = Physics2D.RaycastAll(ball.transform.position, ball.transform.position + Vector3.down, ball.GetComponent<CircleCollider2D>().radius * 2);
            //Debug.Log(ball.GetComponent<CircleCollider2D>().radius);
            Debug.DrawLine(ball.transform.position, (ball.transform.position + Vector3.down * ball.GetComponent<CircleCollider2D>().radius*3));
            bool collideWithSomething = false;
            foreach (RaycastHit2D verticalRaycastHit in verticalRaycastHits)
            {
                Debug.Log(verticalRaycastHit.distance);
                Debug.Log(verticalRaycastHit.collider.tag);
                if (verticalRaycastHit.collider.tag != "Ball")
                {
                    collideWithSomething = true;
                }
            }*/
            return ballForce >= 0.2f;
        } else
        {
            Debug.Log("PlayerIsMoving : ball = null !");
            return false;
        }
    }

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

    private IEnumerator TeleportPlayerIfStill()
    {
        yield return new WaitForSeconds(0.2f);
        if (!PlayerIsMoving())
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (character != null && ball != null)
        {
            Vector3 newCharacterPosition = ball.transform.position + new Vector3(0, -ball.GetComponent<CircleCollider2D>().bounds.size.y / 2);
            float distance = Mathf.Sqrt(Mathf.Pow(character.transform.position.x - newCharacterPosition.x, 2) + Mathf.Pow(character.transform.position.y - newCharacterPosition.y, 2));
            if (character.transform.position != newCharacterPosition && exitWarp == null && distance >= 0.5f)
            {
                StartCoroutine(TeleportAndAnimate(newCharacterPosition));
            }
        } else
        {
            Debug.Log("TeleportPlayer : character || ball = null !");
        }
    }

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

    private IEnumerator DestroyAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(exitWarp);
        Destroy(enterWarp);
        isTeleporting = false;
    }

    public Vector2 GetLastStablePosition()
    {
        return lastStablePosition;
    }

}
