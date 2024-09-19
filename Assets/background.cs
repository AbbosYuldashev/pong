using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource musicSource;  // Reference to the AudioSource component
    public AudioClip backgroundMusic;  // Assign the background music clip

    void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;  // Loop the music
            musicSource.Play();  // Start playing the background music
        }
    }
}
