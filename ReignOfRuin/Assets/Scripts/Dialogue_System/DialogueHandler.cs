using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler _Instance { get; private set; }

    public Dialogue testDialogue; 

    public Transform dialogueTrans;
    public TextMeshProUGUI txtToScreen;
    public ProceedButton pB;

    public float delay = 0.1f;
    public GameObject canvas;  

    public bool stopWriting;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

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

        dialogueTrans = GameObject.Find("DialogueObject").transform;
        txtToScreen = dialogueTrans.GetChild(1).GetComponent<TextMeshProUGUI>();
        pB = dialogueTrans.GetChild(2).GetComponent<ProceedButton>();

        testDialogue.inx = 0;        
        StartCoroutine(TypeWriter(testDialogue.dialogueSequence[testDialogue.inx], testDialogue));
    }

    public void SpeechProceed()
    {
        if (isTyping)
        {
            //Finish the line instantly
            stopWriting = true;
            return;
        }

        testDialogue.inx++;

        if (testDialogue.inx >= testDialogue.dialogueSequence.Count)
        {
            pB.CompleteProceedButton();
            return;
        }

        if (testDialogue.dialogueSequence[testDialogue.inx] == null)
        {
            pB.CompleteProceedButton();
            return;
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeWriter(testDialogue.dialogueSequence[testDialogue.inx], testDialogue));
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        SpeechProceed();
    //}

    //put an interrupt here so its not janky
    public IEnumerator TypeWriter(string dialogue, Dialogue testDialogue) {
        stopWriting = false;
        isTyping = true;
        int curInx = testDialogue.inx;
        string fullText = testDialogue.dialogueSequence[curInx];
        txtToScreen.text = "";

        for (int i = 0; i <= fullText.Length; i++)
        {
            if (stopWriting)
            {
                txtToScreen.text = fullText; // Immediately reveal full text
                break;
            }

            txtToScreen.text = fullText.Substring(0, i);

            if (stopWriting) yield break;
            yield return new WaitForSeconds(delay);
        }

        stopWriting = false;
        isTyping = false;
    }
}
