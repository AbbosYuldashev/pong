using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AI_paddle : MonoBehaviour
{
 
    public Transform ball;  // Reference to the ball's Transform
    public float speed = 5f;  // Speed at which the AI paddle moves
    public float reactionTime = 0.05f;  // How fast the AI reacts (smaller = faster reaction)
    public float movementThreshold = 0.2f;  // Distance within which the AI moves towards the ball

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (ball == null)
        {
            Debug.LogError("Ball reference is missing! Please assign the Ball in the Inspector.");
        }
    }

    void FixedUpdate()
    {
        if (ball != null)
        {
            // Move the paddle only if the ball is above or below the paddle by a threshold
            if (Mathf.Abs(ball.position.y - transform.position.y) > movementThreshold)
            {
                Vector2 targetPosition = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, ball.position.y, reactionTime));
                rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime));
            }
        }
    }
}
