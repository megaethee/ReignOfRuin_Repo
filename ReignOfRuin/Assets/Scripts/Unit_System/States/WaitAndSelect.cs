using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class WaitAndSelect : MonoBehaviour, UnitInterface
{
    public TeleCords tC;

    TextMeshProUGUI displayCordsText;

    public GameObject startTileUI;
    //private GameObject startTileObj; 
    private GameObject canvas;

    private void Awake()
    {}

    public void Again()
    {
        //startTileObj = Instantiate(startTileUI, startTileUI.transform.position, startTileUI.transform.rotation, DialogueHandler._Instance.canvas.transform);

        startTileUI = GameObject.Find("InteractionUI").transform.GetChild(2).gameObject;
        startTileUI.SetActive(true);

        displayCordsText = startTileUI.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    { 
        displayCordsText.text = $"{tC.teleCords.x}, {tC.teleCords.y}";

        if (Input.GetKeyDown(KeyCode.E)) {
            tC.teleCords.x++;
            if (tC.teleCords.x > GridManager._Instance.gridSize.x-1)
                tC.teleCords.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            tC.teleCords.x--;
            if (tC.teleCords.x < 0)
                tC.teleCords.x = GridManager._Instance.gridSize.x-1;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.parent.GetComponent<UnitHandler>().StateProceed();
        }
    }
   
   public void DestroyUI()
   {
        transform.parent.gameObject.tag = "PlayerTroop";
        //Destroy(startTileObj);
        startTileUI.SetActive(false);
        PlayerStates._Instance.isEngaged = false; 
   } 
}
