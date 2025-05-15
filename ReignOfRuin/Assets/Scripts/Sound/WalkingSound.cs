using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WalkingSoundController : MonoBehaviour
{
    [Tooltip("Array of footstep sounds to randomly choose from.")]
    public AudioClip[] walkSounds;

    [Tooltip("Minimum time (in seconds) between each footstep sound.")]
    public float stepInterval = 0.4f;

    [Tooltip("If true, slightly vary the pitch of each footstep for realism.")]
    public bool randomizePitch = true;

    [Tooltip("Range of pitch variation (e.g., 0.95 to 1.05).")]
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    [Tooltip("Volume of the footstep sounds (0 = silent, 1 = full volume).")]
    [Range(0f, 1f)]
    public float footstepVolume = 1f;

    private AudioSource audioSource; // Reference to the attached AudioSource
    private bool isMoving = false;   // Whether the player is currently moving
    private float stepTimer = 0f;    // Timer to control step intervals

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Setup: make sure footsteps don't loop or play on awake
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Check movement input (WASD or arrow keys)
        isMoving = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayRandomFootstep();
                stepTimer = stepInterval; // Reset timer
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    // Plays a randomly chosen footstep sound
    void PlayRandomFootstep()
    {
        if (walkSounds.Length == 0)
        {
            Debug.LogWarning("WalkingSoundController: No footstep sounds assigned.");
            return;
        }

        // Select a random clip from the array
        AudioClip clip = walkSounds[Random.Range(0, walkSounds.Length)];

        // Apply pitch variation if enabled
        audioSource.pitch = randomizePitch ? Random.Range(pitchRange.x, pitchRange.y) : 1f;

        // Play clip with volume control
        audioSource.PlayOneShot(clip, footstepVolume);
    }
}
