using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler _Instance { get; private set; }

    public Dialogue testDialogue; 
    public TextMeshProUGUI txtToScreen;
    public float delay = 0.1f;
    public GameObject canvas; 

    private void Awake()
    {
        if (null == _Instance)
            _Instance = this;
        else Destroy(gameObject); 

        canvas = GameObject.Find("Canvas"); 
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Begin(Dialogue dialogue)
    {
        testDialogue = dialogue;
        txtToScreen = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        testDialogue.inx = 0;        
        StartCoroutine(TypeWriter(testDialogue.dialogueSequence[testDialogue.inx], testDialogue));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow)) { 
            //testDialogue.inx++;
            
            //if (testDialogue.inx >= testDialogue.dialogueSequence.Capacity)
                //return;
            //else  
                //if (testDialogue.dialogueSequence[testDialogue.inx] == null) 
                    //return; 
                //else
                    //StartCoroutine(TypeWriter(testDialogue.dialogueSequence[testDialogue.inx]));
        //}
    }

    public IEnumerator TypeWriter(string dialogue, Dialogue testDialogue) {
        int curInx = testDialogue.inx; 
        for (int i=0; i<testDialogue.dialogueSequence[curInx].Length+1; i++) {
            txtToScreen.text = testDialogue.dialogueSequence[curInx].Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }
}
