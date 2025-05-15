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
    private GameObject startTileObj; 
    private GameObject canvas;

    private void Awake()
    {}

    public void Again()
    {
        startTileObj = Instantiate(startTileUI, startTileUI.transform.position, startTileUI.transform.rotation, DialogueHandler._Instance.canvas.transform);
        
        displayCordsText = startTileObj.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    { 
        displayCordsText.text = $"{tC.teleCords.x}, {tC.teleCords.y}";

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            tC.teleCords.x++;
            if (tC.teleCords.x > GridManager._Instance.gridSize.x-1)
                tC.teleCords.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            tC.teleCords.x--;
            if (tC.teleCords.x < 0)
                tC.teleCords.x = GridManager._Instance.gridSize.x-1;
        }
    }
   
   public void DestroyUI()
   {
        transform.parent.gameObject.tag = "PlayerTroop";
        Destroy(startTileObj);
        PlayerStates._Instance.isEngaged = false;
   } 
}
