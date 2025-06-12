using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController _Instance;
    private UIManager uiManager; // Reference to the UIManager
    private string sceneName;
    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogError("UIManager not found in the scene.");
            }
        }
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void HandleButtonPress(string buttonName)
    {
        // Handle the button press based on the buttonName
        switch (buttonName)
        {
            case "StartGame":
                LoadScene("GameScene");
                break;
            case "QuitGame":
                ExitGame();
                break;
            case "Controls":
                uiManager.HandleUISwitch("Controls");
                break;
            case "MainMenu":
                uiManager.HandleUISwitch("MainMenu");
                break;
            default:
                Debug.LogWarning($"Button '{buttonName}' not recognized.");
                break;
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
