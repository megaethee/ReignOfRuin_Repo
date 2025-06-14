using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager _Instance { get; private set; }

    [System.Serializable] public struct MiniGame {
        public int lvl;
        public GameObject mgObj;
    }
    [System.Serializable] public struct MiniGames {
        public string name;
        public List<MiniGame> miniGames;

        public MiniGames(string name, List<MiniGame> miniGames) {
            this.name = name;
            this.miniGames = miniGames;
        }
    } 
    public List<MiniGames> mgDataBase = new List<MiniGames>(); 

    public int gameLvl=0;

    public Vector3 curPosition;

    private void Awake()
    {
        if (null == _Instance)
            _Instance = this;
        else
            Destroy(gameObject);
    }
//how to randomize this though...
    public void InitMinigame(int x, UnitHandler sH)
    { 
        gameLvl = x;

        if (sH == null) Debug.Log("sH is null");

        switch (sH.unitType)
        {
            case UnitHandler.UnitType.Peasant:
                Debug.Log("I am peasant");
                StartMiniGame(mgDataBase[0].miniGames[gameLvl-1]);
                break;
            case UnitHandler.UnitType.Archer:
                //Debug.Log("I am archer");
                //sH.StateProceed();
                StartMiniGame(mgDataBase[1].miniGames[gameLvl-1]);
                break;
            case UnitHandler.UnitType.Blacksmith:
                Debug.Log("I am blacksmith");
                StartMiniGame(mgDataBase[2].miniGames[gameLvl-1]);
                break;
            case UnitHandler.UnitType.Drunkard:
                StartMiniGameCenter(mgDataBase[3].miniGames[gameLvl-1]);
                break;
            case UnitHandler.UnitType.Wizard:
                //Debug.Log("I am a wizard");
                //sH.StateProceed();
                StartMiniGame(mgDataBase[4].miniGames[gameLvl-1]);
                break;
        } 
        
       //StartMiniGame();

    }

    private void StartMiniGame(MiniGame mG)
    {
        //instantiate minigame object here
        Vector3 offset = new Vector3(0, 0, 0);
        if (curPosition.x < 4)
            offset = new Vector3(2f, 0, 0);

        GameObject miniGamePref = Instantiate(mG.mgObj, curPosition + offset, mG.mgObj.transform.rotation);  
    } 
//starts minigames that happen in center of town
    private void StartMiniGameCenter(MiniGame mG)
    {
        GameObject miniGamePref = Instantiate(mG.mgObj, new Vector3(0, 0, -12), mG.mgObj.transform.rotation);
    }
}
