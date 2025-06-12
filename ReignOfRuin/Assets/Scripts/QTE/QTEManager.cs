using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class QTEManager : MonoBehaviour
{
    // UI
    public TextMeshProUGUI cueText;               // Displays "Hit the Anvil!" and other feedback
    public TextMeshProUGUI scoreText;             // Displays current score during the minigame

    // Audio
    public AudioSource hammerAudio;               // Plays hammer sound on successful hit
    public AudioSource missAudio;                 // Plays when the player misses
    public AudioSource minigameAmbience;          // Optional ambience played during minigame
    public AudioSource areaAmbience;              // Blacksmith area's ambient sound (fades/stops during minigame)

    // QTE Settings
    public float hitWindow = 1f;                        // Time allowed to press space
    public Vector2 cueDelayRange = new Vector2(1f, 3f); // Random delay before cue appears
    public int maxAttempts = 5;                         // How many prompts total

    // References
    public GameObject player, anvilTrigger;             // Reference to player GameObject
    public PlayerController playerMovementScript;       // Player movement script to disable during minigame
    public UnitHandler sH;

    // Events
    public UnityEvent<int> onMinigameCompleted;         // Event callback with final score 

    // Internal state
    private int currentAttempts = 0;
    private int score = 0;
    private bool canHit = false;
    private bool gameActive = false;
    private float timer = 0f;

    void Awake()
    {
        // Make sure none of the prompt text and score text UI is visible on game start
        // cueText.gameObject.transform.parent.gameObject.SetActive(false);
        cueText.text = "";
        scoreText.text = "";

    }

    void Update()
    {
        // Note to self: || means OR
        // "If either gameActive is false OR canHit is false, then skip the code inside this block."
        // If either one is not true, then the Update shouldn't work
        if (!gameActive || !canHit) return;

        timer += Time.deltaTime; 

        // Check for spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timer <= hitWindow)
                HitSuccess(); // Successful hit within timing window
            else
                HitMissed();  // Late hit
        }

        // If no input and time runs out, count as miss
        if (timer > hitWindow)
            HitMissed();
    }

    // Starts the QTE minigame. It should be called by the trigger or interactable.
    public void StartMinigame()
    {
        cueText.text = "";

        // Prevent re-triggering if game already running
        if (gameActive) return;

        if (GameObject.FindWithTag("Station") != null)
        {
            sH = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
            sH.minigameStarted = true;
        }
    
        anvilTrigger = GameObject.FindWithTag("AnvilTrigger");
        maxAttempts = anvilTrigger.GetComponent<AnvilTrigger>().maxAttemptsLevel;
        cueText.gameObject.transform.parent.gameObject.SetActive(true);
        gameActive = true;
        currentAttempts = 0;
        score = 0;
        UpdateScoreUI();
        cueText.text = "";

        // Stop area ambient sound
        if (areaAmbience != null && areaAmbience.isPlaying)
            areaAmbience.Stop();

        // Start minigame-specific ambience
        if (minigameAmbience != null && !minigameAmbience.isPlaying)
            minigameAmbience.Play();

        FreezePlayer(true); // Prevent movement during minigame

        StartCoroutine(WaitAndShowCue()); // Start first cue after random delay
    }

    // Called when player hits space within timing window
    void HitSuccess()
    {
        hammerAudio.Play();
        cueText.color = Color.green;
        cueText.text = "Nice!";
        
        // Note to self: ++ is like adding 1 to whatever variable you have
        // Two kinds, Post and Pre
        // Example of post: int x = 5, int y = x++
        // y = 5 and x becomes 6
        // Value first, then increment
        // Example of pre: int x = 5, int y = ++x
        // y = 6 and x is 6
        // Increment first, then use that value
        score++;
        currentAttempts++;
        canHit = false;

        UpdateScoreUI();

        if (currentAttempts >= maxAttempts)
            StartCoroutine(DelayedEndMinigame());
        else
            StartCoroutine(WaitBeforeNextCue());
    }

    // Called when player misses or hits too late
    void HitMissed()
    {
        cueText.color = Color.red;
        cueText.text = "Miss!";

        // Play miss sound
        // Reminder: && is AND
        // if both are true, return true. If one is false, return false
        if (missAudio != null && !missAudio.isPlaying)
            missAudio.Play();

        currentAttempts++;
        canHit = false;

        StartCoroutine(FailAndEnd());
        return;

    }

    // Displays cue and enables hit detection
    void ShowCue()
    {
        cueText.color = Color.white;
        cueText.text = "Hit the Anvil! (Spacebar)";
        timer = 0f;
        canHit = true;
    }

    // Waits randomly before showing next cue
    IEnumerator WaitAndShowCue()
    {
        yield return new WaitForSeconds(Random.Range(cueDelayRange.x, cueDelayRange.y));
        ShowCue();
    }

    // Waits briefly before showing next cue, to give time for "Nice!" or "Miss!" to show
    IEnumerator WaitBeforeNextCue()
    {
        yield return new WaitForSeconds(1f); // Wait 1 second to show feedback
        StartCoroutine(WaitAndShowCue());      // Then prepare next cue
    }

    // Waits briefly before ending, to show final result
    IEnumerator DelayedEndMinigame()
    {
        yield return new WaitForSeconds(1.0f);
        EndMinigame();
    }

    IEnumerator FailAndEnd()
    {
        yield return new WaitForSeconds(1f);    // Brief pause to show "Miss!"

        EndMinigame();                          // End current session
  
    }

    // Ends the minigame, restores player control and ambience, triggers event
    void EndMinigame()
    {
        cueText.text = "";
        cueText.color = Color.white;

        // Note to self: how $"..." {}/{} works
        // $"..." lets C# know that 
        // "This is a string with embedded variables — replace the stuff in {} with actual values."
        // Score gets replaced with whatever the value is for the score variable
        // Same deal with maxAttempts
        // So, if the score was 3 and maxAttempts was 5, it should print out "Done! Score: 3/5"
        // Called String Interpolation. The $ is called a string interpolation prefix 
        cueText.text = $"Done! Score: {score}/{maxAttempts}";
        gameActive = false;
        canHit = false;

        // Stop minigame ambience
        if (minigameAmbience != null && minigameAmbience.isPlaying)
            minigameAmbience.Stop();

        // Resume area ambience
        if (areaAmbience != null && !areaAmbience.isPlaying)
            areaAmbience.Play();

        FreezePlayer(false); // Re-enable player movement

        // Clear the "Done!" text after 2 seconds
        StartCoroutine(ClearCueTextAfterDelay());

        onMinigameCompleted?.Invoke(score); // Notify listeners

        if (score < maxAttempts)
            sH.StateReset();
        else
            sH.StateProceed();

        //cueText.gameObject.transform.parent.gameObject.SetActive(false);

        Destroy(anvilTrigger);
    }

    // Clears cue text after a short delay
    IEnumerator ClearCueTextAfterDelay(float delay = 2f)
    {
        cueText.text = "";
        yield return new WaitForSeconds(delay);

        scoreText.text = "";
    }

    // Updates on-screen score display
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}/{maxAttempts}";
    }

    // Enables or disables player movement
    void FreezePlayer(bool freeze)
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = !freeze;
    }

    // Enables other scripts to check if the game is active (used by AnvilTrigger to prevent reactivation mid-game)
    public bool IsGameActive()
    {
        return gameActive;
    }
}