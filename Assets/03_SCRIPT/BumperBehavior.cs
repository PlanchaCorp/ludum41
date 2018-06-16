using UnityEngine;


/// <summary>
/// Bumper action class
/// </summary>
public class BumperBehavior : MonoBehaviour {
    /// <summary>
    /// Force of the bump
    /// </summary>
    public float bumpPower = 1000;

    /// <summary>
    /// Bump when ball comes in contact
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball" && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            GetComponent<Animator>().SetTrigger("bump");
            collision.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.rotation * new Vector2(0, bumpPower));
        }
    }
}
