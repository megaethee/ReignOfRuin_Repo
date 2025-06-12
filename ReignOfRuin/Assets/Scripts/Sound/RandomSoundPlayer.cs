using UnityEngine;

// This attribute ensures that the GameObject has an AudioSource component.
// If it doesn't, Unity will automatically add one when you add this script.
[RequireComponent(typeof(AudioSource))]
public class RandomSoundPlayer : MonoBehaviour
{
    // Public array of audio clips that be can assigned in the Unity Inspector.
    public AudioClip[] clips;

    // If true, the pitch of the audio will slightly change every time a clip plays.
    public bool randomizePitch = true;

    // Defines the min and max range for the pitch variation (if enabled).
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    // Controls the volume at which the sound plays.
    // It's clamped between 0 (silent) and 1 (full volume) in the Inspector.
    [Range(0f, 1f)]
    public float volume = 1f;

    // Reference to the AudioSource component attached to the GameObject.
    private AudioSource audioSource;

    // Called when the GameObject is created (before Start).
    void Awake()
    {
        // Get the AudioSource component on the same GameObject.
        audioSource = GetComponent<AudioSource>();

        // Ensure the sound doesn't loop endlessly.
        audioSource.loop = false;

        // Prevent the sound from playing automatically when the GameObject is enabled.
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        // Play a random sound clip as soon as the object spawns.
        PlayRandomClip();
    }

    public void PlayRandomClip()
    {
        // If there are no clips assigned, log a warning and stop.
        if (clips.Length == 0)
        {
            Debug.LogWarning("RandomSoundPlayer: No audio clips assigned.");
            return;
        }

        // Pick a random clip from the array.
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        // Randomly adjust pitch if enabled.
        // Example of a ternary operator in C#
        // Its like a if else statement but condensed
        // If randomizePitch is true, choose a random pitch within the pitchRange;
        // otherwise, set pitch to 1 (normal pitch).
        audioSource.pitch = randomizePitch ? Random.Range(pitchRange.x, pitchRange.y) : 1f;

        // Play the selected clip once, at the specified volume.
        audioSource.PlayOneShot(clip, volume);
    }
}
