using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehavior : MonoBehaviour {
    public float bumpPower = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball" && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            GetComponent<Animator>().SetTrigger("bump");
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bumpPower));
        }
    }
}
