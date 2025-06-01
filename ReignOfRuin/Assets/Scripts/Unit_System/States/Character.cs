using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour, UnitInterface
{ 
   //public CameraZoomManager cameraZoomManager;
   
   public GameObject dialogueUI;
   public List<Dialogue> randDialogue = new List<Dialogue>();
   public Rigidbody rB;
   //private GameObject dialogueObj; 
   private UnitHandler unitHandler;

   GameObject canvas;  

   private void Awake()
   {  
      Again();
   }  

   public void Again()
   {
      transform.parent.tag = "Untagged";   
      
      if (dialogueUI == null)
         dialogueUI = GameObject.Find("InteractionUI").transform.GetChild(0).gameObject;
      canvas = GameObject.Find("Canvas");  

      //unitHandler = transform.parent.GetComponent<UnitHandler>();
      StartCoroutine(WaitForInstance());

   }

   private IEnumerator WaitForInstance()
   {
      while (transform.parent.GetComponent<UnitHandler>().animator == null)
         yield return null;

      unitHandler = transform.parent.GetComponent<UnitHandler>();
      StartCoroutine(Orbit());
   }

   private void OnTriggerEnter(Collider other)
   {
      //Debug.Log("Collided");
      if (other.tag == "Player" && !PlayerStates._Instance.isEngaged)
      {

         transform.parent.gameObject.tag = "PlayerUnit";

         DialogueEngaged();
      }
   }  

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player") {
         //cameraZoomManager.FollowPlayerYOnly();
         //cameraZoomManager.ResetCamera();
         Again(); 
         unitHandler.ranInto = false;
         PlayerStates._Instance.isEngaged = false;
         dialogueUI.SetActive(false);  
         unitHandler.imEngaged = false;

         StartCoroutine(Orbit());
      }
   } 

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space) && unitHandler.imEngaged)
         DialogueHandler._Instance.SpeechProceed();
   }

   private IEnumerator Orbit()
   {
      //no need for lerp with Rigidbody.MovePosition() 
      unitHandler.animator.SetBool("isMoving", true);

      float elapsedTime=0, hangTime=2f;
      Vector3 curPos = transform.parent.parent.position;

      Vector3 targPos = new Vector3(Random.Range(curPos.x-3, curPos.x+3), transform.parent.parent.position.y, Random.Range(curPos.z-3, curPos.z+3));

      transform.parent.parent.eulerAngles = Quaternion.LookRotation(targPos-curPos).eulerAngles;

      while (elapsedTime < hangTime)
      {
         if (unitHandler.imEngaged == true || unitHandler.ranInto == true)
            yield break;

         transform.parent.parent.position = Vector3.Lerp(curPos, targPos, (elapsedTime / hangTime));

         //rB.MovePosition(curPos + (targPos-curPos) * (elapsedTime/hangTime));

         elapsedTime += Time.deltaTime;
         yield return null;
      }

      unitHandler.animator.SetBool("isMoving", false);

      yield return new WaitForSeconds(1f);
      StartCoroutine(Orbit());
   }

   private void DialogueEngaged()
   {
      if (GameObject.FindWithTag("InteractUI") == null) {
         unitHandler.animator.SetBool("isMoving", false); 
         dialogueUI.SetActive(true);
         DialogueHandler._Instance.Begin(randDialogue[Random.Range(0, randDialogue.Count)]); 
         PlayerStates._Instance.isEngaged = true;
         unitHandler.imEngaged = true;
      }
   }

   public void DestroyUI()
   { 
      StopCoroutine(Orbit());
      //Destroy(dialogueObj);
      dialogueUI.SetActive(false);

      unitHandler.animator.SetBool("isMoving", false);

      PlayerStates._Instance.isEngaged = false;
      //unitHandler.imEngaged = false;
        
   }
}
