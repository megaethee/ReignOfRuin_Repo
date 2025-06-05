using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Stronghold : MonoBehaviour
{
    public float health = 10, baseHealth;
    public GameObject winLoseUI, inGame;
    public Slider healthSlider;

    // Update is called once per frame
    void Awake()
    {
        //if (tag == "PlayerStronghold")
            //healthSlider.maxValue = health;
        baseHealth = health;
        winLoseUI = GameObject.Find("WinLose");
        inGame = GameObject.FindWithTag("InGame");
    }

    void Update()
    {
        if (tag == "PlayerStronghold")
            healthSlider.value = health/baseHealth;
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
