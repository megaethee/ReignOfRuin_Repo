using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _Instance;
    private GameManager _gameManager;
    private SceneController _sceneController;
    public MainMenuManager _mainMenuManager;
    public bool isInMainMenu = true; // Set this to true if the UIManager is used in the main menu scene
    private string _currentUI = "MainMenu", _oldUI;

    [Header("Assign all UI panels here")]
    public List<GameObject> UIPanels;
    private void Awake()

    {

        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
            if (_gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene.");
            }
        }
        if (_sceneController == null)
        {
            _sceneController = FindObjectOfType<SceneController>();
            if (_sceneController == null)
            {
                Debug.LogError("SceneController not found in the scene.");
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
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshUIPanels();
    }
    private void Start()
    {
        //RefreshUIPanels();
    }

    public void HandleUISwitch(string uiName)
    {
        switch (uiName)
        {
            case "StartGame":
                Debug.Log("Switching to Game UI");

                _sceneController.LoadScene("GameScene");
                _currentUI = "GameUI";
                isInMainMenu = false; // Set to false since we are now in the game scene

                break;
            case "PauseMenu":
                Debug.Log("Switching to Main Menu UI");
                ShowUI("PauseMenu");
                _gameManager.PauseGame(); // Ensure the game is paused when switching to Pause Menu
                break;
            case "GameUI":
                Debug.Log("Switching to Game UI");
                ShowUI("GameUI");
                _gameManager.ResumeGame(); // Ensure the game is not paused when switching to Game UI
                break;
            case "Controls":
                Debug.Log("Switching to Controls UI");
                ShowUI("Controls");

                break;
            case "MainMenu":
                Debug.Log("Switching to Main Menu UI");
                ShowUI("MainMenu");

                break;
            case "BackToMainMenu":
                Debug.Log("Switching to Main Menu UI");
                isInMainMenu = true; // Set to true since we are returning to the main menu
                _sceneController.LoadScene("MainMenu");
                

                ShowUI("MainMenu");
                break;
            case "QuitGame":
                Debug.Log("Exiting game...");
                Application.Quit();
                break;
            default:
                Debug.LogWarning($"UI '{uiName}' not recognized.");
                return; // Exit if the UI name is not recognized
        }


        _oldUI = _currentUI;
        _currentUI = uiName;

        //ShowUI(_currentUI);
    }
    private void ShowUI(string uiName)
    {
        foreach (var panel in UIPanels)
        {
            if (panel != null)
                panel.SetActive(panel.name == uiName);
        }
    }
    private void RefreshUIPanels()
    {
        if (isInMainMenu)
        {
            _mainMenuManager = FindObjectOfType<MainMenuManager>();
            if (_mainMenuManager != null)
            {
                UIPanels.AddRange(_mainMenuManager.UIPanels);
            }
            ShowUI("MainMenu");
        }
        //UIPanels = new List<GameObject>();
        else
        {
            UIPanels.Clear();
            GameObject[] panels = GameObject.FindGameObjectsWithTag("UIPanel");
            Debug.Log($"Found {panels.Length} UI panels in the scene.");
            UIPanels.AddRange(panels);
            ShowUI(_currentUI);
        }
        

        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
