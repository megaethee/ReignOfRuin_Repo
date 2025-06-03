using System.Collections;
using UnityEngine;

// Setting it up:
// Create a GameObject to define the sound area.
//  -Add a Box Collider and enable "Is Trigger".
// Create another GameObject with this script and an AudioSource component.
//  -Set AudioSource: loop = true, playOnAwake = false, spatialBlend = 1 (for 3D sound).
// Assign the Area collider and the Player GameObject in the Inspector.
// If there is an issue with sound not working, check if rigidbody is assigned to player. Happened to me.

[RequireComponent(typeof(AudioSource))]
public class AmbienceSound : MonoBehaviour
{
    [Tooltip("Trigger collider that defines the area where the ambience is active.")]
    public Collider Area;

    [Tooltip("GameObject representing the player to track.")]
    public GameObject Player;

    [Tooltip("Time in seconds to fade in/out the audio when entering/exiting.")]
    public float fadeDuration = 1f;

    private AudioSource audioSource;          // Reference to the attached AudioSource
    private Coroutine fadeCoroutine;          // Stores currently running fade coroutine
    private bool isPlayerInside = false;      // Tracks if player is currently inside the trigger

    void Start()
    {
        // Get and store the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Basic sanity checks to warn developers
        if (Area == null)
            Debug.LogWarning("AmbienceSound: Area collider is not assigned.");

        if (Player == null)
            Debug.LogWarning("AmbienceSound: Player GameObject is not assigned.");

        if (Area != null && !Area.isTrigger)
            Debug.LogWarning("AmbienceSound: Area collider should be set to 'Is Trigger'.");
    }

    void Update()
    {
        // Do nothing if setup is incomplete
        if (Area == null || Player == null)
            return;

        // Always position this GameObject (with the AudioSource) at the point closest to the player
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        transform.position = closestPoint;
    }

    // Called by Unity when any collider enters this GameObject's trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if it's the player entering
        if (other.gameObject == Player && !isPlayerInside)
        {
            isPlayerInside = true;
            StartFadeIn(); // Begin fading in the sound
        }
    }

    // Called by Unity when any collider exits this GameObject's trigger
    void OnTriggerExit(Collider other)
    {
        // Check if it's the player exiting
        if (other.gameObject == Player && isPlayerInside)
        {
            isPlayerInside = false;
            StartFadeOut(); // Begin fading out the sound
        }
    }

    // Starts the fade-in process
    void StartFadeIn()
    {
        // Stop any existing fade before starting a new one
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Start fading volume from 0 to 1
        fadeCoroutine = StartCoroutine(FadeAudio(0f, 1f));
    }

    // Starts the fade-out process
    void StartFadeOut()
    {
        // Stop any existing fade before starting a new one
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Start fading volume from current value to 0, and stop audio after fade
        fadeCoroutine = StartCoroutine(FadeAudio(audioSource.volume, 0f, stopAfter: true));
    }

    // Smoothly fades audio volume between two values over time
    IEnumerator FadeAudio(float startVolume, float targetVolume, bool stopAfter = false)
    {
        // If we're fading in and audio isn't already playing, start it
        if (!audioSource.isPlaying && targetVolume > 0f)
            audioSource.Play();

        float elapsed = 0f;

        // Gradually change volume over fadeDuration
        while (elapsed < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final volume is set precisely
        audioSource.volume = targetVolume;

        // Stop audio if we're fading out completely
        if (stopAfter && targetVolume == 0f)
            audioSource.Stop();
    }
}
