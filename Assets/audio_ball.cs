using UnityEngine;

public class BallSound : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip collisionSound;  // Assign the sound clip for collisions

    void Start()
    {
        // Check if AudioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing from the ball!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play the sound when the ball collides with anything (walls, paddles, etc.)
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
