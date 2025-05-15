using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference to the RhythmGameManager in the scene
    public QTEManager rhythmMinigame;

    void Start()
    {
        // Subscribe to the minigame's completion event
        // When the minigame finishes, it will call OnMinigameFinished and pass the final score
        rhythmMinigame.onMinigameCompleted.AddListener(OnMinigameFinished);
    }

    // This function runs when the minigame completes
    // It receives the final score as an argument
    void OnMinigameFinished(int score)
    {
        Debug.Log($"Blacksmith minigame complete! Final score: {score}");

        // You can add your own logic here, such as:
        // - Giving a reward based on the score
        // - Updating quest progress
        // - Unlocking a crafting option
        // - Showing a results screen or dialog
    }
}
