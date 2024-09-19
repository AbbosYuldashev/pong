using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class SettingsMenu : MonoBehaviour
{
    public Slider ballSpeedSlider;
    public Text ballSpeedText;

    public Slider paddleSizeSlider;
    public Text paddleSizeText;

    public InputField matchDurationInputField;
    public Text matchDurationText;

    public P1 leftPaddle;  // Reference to the left paddle
    public AI_paddle rightPaddle; // Reference to the right paddle

    void Start()
    {
        UpdateBallSpeedText();
        UpdatePaddleSizeText();
        UpdateMatchDurationText();

        ballSpeedSlider.onValueChanged.AddListener(delegate { UpdateBallSpeedText(); });
        paddleSizeSlider.onValueChanged.AddListener(delegate { UpdatePaddleSizeText(); });
        matchDurationInputField.onEndEdit.AddListener(delegate { UpdateMatchDurationText(); });

        // Initialize paddle size based on current slider value
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("BallSpeed", ballSpeedSlider.value);
        PlayerPrefs.SetFloat("PaddleSize", paddleSizeSlider.value);

        float matchDuration;
        if (float.TryParse(matchDurationInputField.text, out matchDuration))
        {
            PlayerPrefs.SetFloat("MatchDuration", matchDuration);
        }

        PlayerPrefs.Save();
        Debug.Log("Settings Saved!");
    }

    public void StartGame()
    {
        SaveSettings();
        SceneManager.LoadScene("GameScene");  // Replace with your actual gameplay scene name
    }

    void UpdateBallSpeedText()
    {
        ballSpeedText.text = "Ball Speed: " + ballSpeedSlider.value.ToString("F2");
    }

    void UpdatePaddleSizeText()
    {
        paddleSizeText.text = "Paddle Size: " + paddleSizeSlider.value.ToString("F2");
        // Update paddles when size changes
    }

    void UpdateMatchDurationText()
    {
        matchDurationText.text = "Match Duration: " + matchDurationInputField.text;
    }
}
