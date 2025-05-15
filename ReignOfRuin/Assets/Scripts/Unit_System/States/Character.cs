using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour, UnitInterface
{ 
   public GameObject dialogueUI;
   public List<Dialogue> randDialogue = new List<Dialogue>();
   private GameObject dialogueObj; 
   private UnitHandler unitHandler;

   GameObject canvas;  

   private void Awake()
   { 
      unitHandler = transform.parent.gameObject.GetComponent<UnitHandler>();
      StartCoroutine(Orbit());

      Again();
      canvas = GameObject.Find("Canvas");  
   }  

   public void Again()
   {
      transform.parent.gameObject.tag = "Untagged";   
   }

   private void OnTriggerEnter(Collider other)
   { 
      //Debug.Log("Collided");
      if (other.tag == "Player" &&  !PlayerStates._Instance.isEngaged && !unitHandler.imEngaged) { 
         transform.parent.gameObject.tag = "PlayerUnit"; 
         DialogueEngaged(); 
      }  
   }  

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player") {
         Again();
         StartCoroutine(Orbit());
         Destroy(dialogueObj);
         PlayerStates._Instance.isEngaged = false;
         unitHandler.imEngaged = false;
      }
   } 

   private IEnumerator Orbit()
   {
      float elapsedTime=0, hangTime=2f;
      Vector3 curPos = transform.parent.position;

      Vector3 targPos = new Vector3(Random.Range(curPos.x-3, curPos.x+3), transform.parent.position.y, Random.Range(curPos.z-3, curPos.z+3));
      
      while (elapsedTime < hangTime) {
         if (PlayerStates._Instance.isEngaged)
            yield break;

         transform.parent.position = Vector3.Lerp(curPos, targPos, (elapsedTime/hangTime));
         elapsedTime += Time.deltaTime;
         yield return null;
      } 
      
      yield return new WaitForSeconds(1f);
      StartCoroutine(Orbit());
   }

   private void DialogueEngaged()
   {
      if (GameObject.FindWithTag("InteractUI") == null) {
         dialogueObj = Instantiate(dialogueUI, canvas.transform.position, dialogueUI.transform.rotation, canvas.transform); 
         DialogueHandler._Instance.Begin(randDialogue[Random.Range(0, randDialogue.Count)]); 
         PlayerStates._Instance.isEngaged = true;
         unitHandler.imEngaged = true;
      }
   }

   public void DestroyUI()
   { 
      StopCoroutine(Orbit());
      Destroy(dialogueObj);

      PlayerStates._Instance.isEngaged = false;
      unitHandler.imEngaged = false;
        
   }
}
