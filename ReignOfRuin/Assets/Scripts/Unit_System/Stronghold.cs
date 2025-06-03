using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stronghold : MonoBehaviour
{
    public float health = 10, baseHealth;
    public GameObject winLoseUI, inGame;

    // Update is called once per frame
    void Awake()
    {
        baseHealth = health;
        winLoseUI = GameObject.Find("WinLose");
        inGame = GameObject.FindWithTag("InGame");
    }

    void Update()
    {
       if (health <= 0) DestroyState(); 
    
    }

    void DestroyState()
    {
        if (gameObject.tag == "OpponentStronghold") winLoseUI.transform.GetChild(0).gameObject.SetActive(true);
        else if (gameObject.tag == "PlayerStronghold") winLoseUI.transform.GetChild(1).gameObject.SetActive(true);

        winLoseUI.transform.GetChild(2).gameObject.SetActive(true);
        Destroy(inGame); 
    }
}
