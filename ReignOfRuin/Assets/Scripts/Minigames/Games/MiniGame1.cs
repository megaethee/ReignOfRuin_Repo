using UnityEngine;

public class MiniGame1 : MonoBehaviour
{
    public UnitHandler stationHandler; 

    private void Awake()
    { 
        if (GameObject.FindWithTag("Station") != null)
            stationHandler = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
        
        Debug.Log("Minigame started");

        MinigameOver();
    }

    public void MinigameOver()
    {
        stationHandler.StateProceed();
        Destroy(gameObject);
    }
}
