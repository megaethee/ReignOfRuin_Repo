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

        tC.teleCords.x = 0;
        displayCordsText.text = "Pink";
        displayCordsText.color = new Color32(236, 141, 255, 255);
    }


    void Update()
    { 
        //switch (tC.teleCords.x) {
        //    case 0:
        //        displayCordsText.text = "Pink";
        //        displayCordsText.color = new Color32(236, 141, 255, 255);
        //        break;
        //    case 1:
        //        displayCordsText.text = "Yellow";
        //        displayCordsText.color = new Color32(255, 238, 131, 255);
        //        break;
        //    case 2:
        //        displayCordsText.text = "Green";
        //        displayCordsText.color = new Color32(87, 217, 135, 255);
        //        break;
        //}
        ////displayCordsText.text = $"{tC.teleCords.x}, {tC.teleCords.y}";

        //if (Input.GetKeyDown(KeyCode.E)) {
        //    tC.teleCords.x++;
        //    if (tC.teleCords.x > GridManager._Instance.gridSize.x-1)
        //        tC.teleCords.x = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.Q)) {
        //    tC.teleCords.x--;
        //    if (tC.teleCords.x < 0)
        //        tC.teleCords.x = GridManager._Instance.gridSize.x-1;
        //}
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            tC.teleCords.x = 0;
            displayCordsText.text = "Pink";
            displayCordsText.color = new Color32(236, 141, 255, 255);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            tC.teleCords.x = 1;
            displayCordsText.text = "Yellow";
            displayCordsText.color = new Color32(255, 238, 131, 255);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            tC.teleCords.x = 2;
            displayCordsText.text = "Green";
            displayCordsText.color = new Color32(87, 217, 135, 255); 
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
