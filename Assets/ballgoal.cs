using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public int winningScore = 5;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text gameOverText;

    public Ball ball;  // Reference to the ball script
    public GameObject restartButton;  // Reference to the restart button
    public GameObject main_menu;

    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip[] scoreSounds;  // Array of sound clips for each point (0-4 for 5 points)

    void Start()
    {
        if (ball == null)
        {
            Debug.LogError("Ball reference is missing! Please assign the Ball in the Inspector.");
            return;
        }

        restartButton.SetActive(false);  // Hide the restart button at the start
        main_menu.SetActive(false);
        UpdateScoreUI();
        gameOverText.text = "";  // Hide game over text at the start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameOverText.text != "") return;  // Prevent further scoring after game over

        // Check if the ball crosses the left side (Player 2 scores)
        if (other.gameObject.CompareTag("LeftGoal"))
        {
            player2Score++;
            PlayScoreSound(player2Score - 1);  // Play sound based on current score
            CheckForGameOver();
        }
        // Check if the ball crosses the right side (Player 1 scores)
        else if (other.gameObject.CompareTag("RightGoal"))
        {
            player1Score++;
            PlayScoreSound(player1Score - 1);  // Play sound based on current score
            CheckForGameOver();
        }
    }

    void PlayScoreSound(int scoreIndex)
    {
        // Ensure score index is valid and within the array bounds
        if (audioSource != null && scoreSounds != null && scoreIndex >= 0 && scoreIndex < scoreSounds.Length)
        {
            audioSource.PlayOneShot(scoreSounds[scoreIndex]);  // Play the corresponding sound
        }
    }

    void CheckForGameOver()
    {
        if (player1Score >= winningScore)
        {
            gameOverText.text = "Player 1 Wins!";
            EndGame();
        }
        else if (player2Score >= winningScore)
        {
            gameOverText.text = "Player 2 Wins!";
            EndGame();
        }
        else
        {
            ball.ResetBall();  // Reset and continue if no one has won
        }

        UpdateScoreUI();
    }

    void EndGame()
    {
        ball.gameObject.SetActive(false);  // Deactivate the ball
        restartButton.SetActive(true);  // Show the restart button when the game ends
        main_menu.SetActive(true);
    }

    void UpdateScoreUI()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    // This method is called when the restart button is clicked
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
