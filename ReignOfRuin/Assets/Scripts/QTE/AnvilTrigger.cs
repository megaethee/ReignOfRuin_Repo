using UnityEngine;
using TMPro;

public class AnvilTrigger : MonoBehaviour
{
    public QTEManager QTEGame;                          // Reference to the QTEManager 
    public TextMeshProUGUI interactionPromptText;       // Reference to the prompt text UI, basically the thing titled blacksmith interaction under canvas

    private bool playerNearby = false;                  // Tracks whether player is inside the anvil trigger zone

    void Awake()
    {
        QTEGame = GameObject.Find("QTE").GetComponent<QTEManager>();
        interactionPromptText = GameObject.Find("BlacksmithInteraction").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Player presses space near anvil to start minigame
        // Only triggers if the minigame is not already active
        if (!QTEGame.IsGameActive() && playerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            QTEGame.StartMinigame();

            // Clear the prompt text after starting the minigame
            if (interactionPromptText != null)
                interactionPromptText.text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Player enters anvil zone
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            // Show prompt if minigame isn't already running
            if (!QTEGame.IsGameActive() && interactionPromptText != null)
                interactionPromptText.text = "Press Space to Start Smithing";
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Player leaves anvil zone
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            // Hide the interaction prompt when player walks away
            if (interactionPromptText != null)
                interactionPromptText.text = "";
        }
    }
}