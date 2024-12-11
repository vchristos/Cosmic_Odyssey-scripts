using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    
    public AudioSource musicAudioSource; 
    private bool isPlaying = true; 

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

        // playing state
        isPlaying = !isPlaying;
    }
}
