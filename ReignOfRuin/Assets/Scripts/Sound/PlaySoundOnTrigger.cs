using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioSource audioSource; // The audio source to play
    public AudioClip soundClip;     // The sound to play on trigger

    private bool hasPlayed = false; // prevent repeated plays

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (audioSource != null && soundClip != null)
            {
                audioSource.PlayOneShot(soundClip);
                hasPlayed = true;
            }
        }
    }
}