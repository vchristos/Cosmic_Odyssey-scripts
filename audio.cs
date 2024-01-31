using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    
    public AudioSource musicAudioSource; // Reference to your music AudioSource
    private bool isPlaying = true; // Variable to track whether the music is currently playing

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            // Pause the music
            musicAudioSource.Pause();
        }
        else
        {
            // Resume the music
            musicAudioSource.UnPause();
        }

        // Toggle the playing state
        isPlaying = !isPlaying;
    }
}
