using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonCycler : MonoBehaviour
{
    public int cIndex=1;
    public int maxButtons;
    public Transform selectRing;

    private void Awake() 
    {
        maxButtons = transform.childCount;
        cIndex = 1;
        selectRing = transform.GetChild(0);
        selectRing.position = transform.GetChild(cIndex).position;
    }
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E)) {
            cIndex++; 
            if (cIndex > maxButtons-1)
                cIndex=1;
       }
       if (Input.GetKeyDown(KeyCode.Q)) {
            cIndex--;
            if (cIndex < 1)
                cIndex=maxButtons-1;
       }
        selectRing.position = transform.GetChild(cIndex).position;

       if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Time for a minigame");
            transform.GetChild(cIndex).GetComponent<ProceedButton>().MinigameProceed();
       }
    }
}
