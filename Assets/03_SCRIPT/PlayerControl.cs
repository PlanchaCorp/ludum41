using UnityEngine.UI;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float BALL_FORCE = 60f;
    public float DISTANCE_MAX = 1f;

    public GameObject arrowPrefab;

    GameObject ball;
    float mouseDownTime = 0f;
    float shootingTime = 0f;
    private GameObject currentArrow;
        
    void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }
	
	void Update () {
        // Positionnement de la camera
        GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = Quaternion.identity;
        // Positionnement de la flèche de tir
        if (mouseDownTime > 0)
        {
            shootingTime = (Time.time - mouseDownTime) * 5;
            PositionArrow(Input.mousePosition, shootingTime);
        }
        // Clic gauche
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time;
            currentArrow = GameObject.Instantiate(arrowPrefab, ball.transform.position, Quaternion.identity);
            currentArrow.transform.parent = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            currentArrow.transform.localScale = new Vector2(currentArrow.transform.localScale.x / 10, currentArrow.transform.localScale.y / 10);
        }
        // Clic droit
        if (Input.GetMouseButtonUp(0))
        {
            ShootBall(Input.mousePosition, shootingTime);
            Destroy(currentArrow);
            mouseDownTime = 0;
        }
    }

    private void ShootBall(Vector2 mousePosition, float elapsedTime)
    {
        // Calcul des distances
        float horizontalDistance = Camera.main.ScreenToWorldPoint(mousePosition).x - ball.transform.position.x;
        float verticalDistance = Camera.main.ScreenToWorldPoint(mousePosition).y - ball.transform.position.y;
        // Application de la fonction sinus
        float horizontalForce = BALL_FORCE * horizontalDistance * (Mathf.Sin(elapsedTime + 3 * Mathf.PI / 2) + 1);
        float verticalForce = BALL_FORCE * verticalDistance * (Mathf.Sin(elapsedTime + 3 * Mathf.PI/2) + 1);
        // Application des forces
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2(horizontalForce, verticalForce));
    }

    private void PositionArrow(Vector2 mousePosition, float elapsedTime)
    {
        // Position and rotate the arrow
        Vector2 localMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        currentArrow.transform.rotation = Quaternion.FromToRotation(Vector3.right, localMousePosition - new Vector2(ball.transform.position.x, ball.transform.position.y));
        currentArrow.transform.position = ball.transform.position + new Vector3(0.35f, 0, 0) + currentArrow.transform.rotation * new Vector2(0.3f, 0.3f);
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
    }
}
