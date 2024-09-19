using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 5f;      // Initial speed of the ball
    public float speedIncrease = 0.5f; // Speed increase on each paddle hit
    public float maxSpeed = 10f;       // Max speed of the ball

    private Rigidbody2D rb;
    private Vector2 ballDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LaunchBall());
    }

    // Coroutine to launch the ball with a random direction
    IEnumerator LaunchBall()
    {
        // Wait before the ball launches
        yield return new WaitForSeconds(1f);
        ResetBall();  // Reset position and set a random direction
    }

    // Reset ball position and direction
    public void ResetBall()
    {
        // Reset position to the center
        transform.position = Vector2.zero;

        // Set a random direction for the ball (either left or right, with a random y-component)
        float randomX = Random.value < 0.5f ? -1f : 1f;
        float randomY = Random.Range(-1f, 1f);
        ballDirection = new Vector2(randomX, randomY).normalized;

        // Apply the initial velocity
        rb.velocity = ballDirection * startSpeed;
    }

    // Handle collision detection with paddles and walls
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle paddle collision
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Reverse the ball's X direction when it hits a paddle
            ballDirection.x *= -1;

            // Adjust the Y direction based on where the ball hit the paddle
            float paddleY = collision.gameObject.transform.position.y;
            float ballY = transform.position.y;
            float paddleHeight = collision.collider.bounds.size.y;

            float yOffset = (ballY - paddleY) / paddleHeight * 2;  // Value between -1 and 1
            ballDirection.y = yOffset;

            // Normalize the direction and increase speed after each paddle hit
            ballDirection = ballDirection.normalized;
            rb.velocity = ballDirection * Mathf.Min(rb.velocity.magnitude + speedIncrease, maxSpeed); // Clamp to max speed
        }

        // Handle top and bottom wall collisions
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse the ball's Y direction when it hits a wall
            ballDirection.y *= -1;
            rb.velocity = ballDirection * rb.velocity.magnitude; // Keep speed consistent, just reverse direction
        }
    }
}
