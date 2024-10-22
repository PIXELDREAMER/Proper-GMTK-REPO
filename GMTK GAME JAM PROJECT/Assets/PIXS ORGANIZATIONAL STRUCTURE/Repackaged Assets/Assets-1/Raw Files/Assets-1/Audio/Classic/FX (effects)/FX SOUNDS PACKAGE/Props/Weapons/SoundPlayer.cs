
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.SoundManagement;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip launchSound;
    public float launchDelay = 0.1f; // Delay before playing the launch sound

    private bool isPlaying = false;

    void Start()
    {
        // Ensure audio source is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource not assigned or found in GameObject.");
                enabled = false; // Disable the script if audio source is not found
            }
        }
    }

    // Method to play the launch sound
    public void PlayLaunchSound()
    {
        if (!isPlaying && launchSound != null)
        {
            StartCoroutine(PlayLaunchSoundCoroutine());
        }
    }

    private IEnumerator PlayLaunchSoundCoroutine()
    {
        isPlaying = true;

        // Delay before playing the sound
        yield return new WaitForSeconds(launchDelay);

        // Play the launch sound
        audioSource.PlayOneShot(launchSound);

        isPlaying = false;
    }
}
