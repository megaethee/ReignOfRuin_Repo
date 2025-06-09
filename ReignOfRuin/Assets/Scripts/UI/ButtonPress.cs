using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private UIManager uiManager;
    [SerializeField] private string buttonName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogError("UIManager not found in the scene.");
            }
        }

    }

    public void OnButtonPress()
    {

        uiManager.HandleUISwitch(buttonName);
    
        if (uiManager == null)
        {
            Debug.LogError("UI Manager is not initialized.");
        }
    }
}
