using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStation : MonoBehaviour, UnitInterface
{ 
   public GameObject dialogueUI;
   public Dialogue dialogue;
   private GameObject dialogueObj; 
   private UnitHandler unitHandler;

   GameObject canvas;  

   private void Awake()
   { 
      unitHandler = transform.parent.gameObject.GetComponent<UnitHandler>(); 

      Again();
      canvas = GameObject.Find("Canvas");  
   }  

   public void Again()
   {
      transform.parent.gameObject.tag = "Untagged";   
   }

   private void OnTriggerEnter(Collider other)
   {    
      if (other.tag == "Player" && !PlayerStates._Instance.isEngaged && !unitHandler.imEngaged) { 
         transform.parent.gameObject.tag = "Station"; 
         DialogueEngaged();          
      } 
   }  

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player") {
         Again();
         Destroy(dialogueObj);
         PlayerStates._Instance.isEngaged = false;
         unitHandler.imEngaged = false;
      }
   }  

   private void DialogueEngaged()
   {
      if (GameObject.FindWithTag("InteractUI") == null) {
         dialogueObj = Instantiate(dialogueUI, canvas.transform.position, dialogueUI.transform.rotation, canvas.transform); 
         DialogueHandler._Instance.Begin(dialogue); 
         PlayerStates._Instance.isEngaged = true;
         unitHandler.imEngaged = true;
      }
   }

   public void DestroyUI()
   {  
      PlayerStates._Instance.isEngaged = false; 
      Destroy(dialogueObj);  
   }
}
