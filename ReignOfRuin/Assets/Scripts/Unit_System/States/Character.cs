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
      unitHandler = transform.parent.gameObject.GetComponent<UnitHandler>();
      //rB = gameObject.GetComponent<Rigidbody>();
      StartCoroutine(Orbit());

      //Again();
      canvas = GameObject.Find("Canvas");  
      //cameraZoomManager = GameObject.Find("Cameras").GetComponent<CameraZoomManager>();
   }  

   public void Again()
   {
      transform.parent.gameObject.tag = "Untagged";   
      
      if (dialogueUI == null)
         dialogueUI = GameObject.Find("InteractionUI").transform.GetChild(0).gameObject;

      
   }

   private void OnTriggerEnter(Collider other)
   { 
      //Debug.Log("Collided");
      if (other.tag == "Player" &&  !PlayerStates._Instance.isEngaged) { 
         //StopCoroutine(Orbit());
         transform.parent.gameObject.tag = "PlayerUnit"; 
         //cameraZoomManager.StopFollowingPlayer();
         //cameraZoomManager.MoveToTarget(stationCamTransform);
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
      float elapsedTime=0, hangTime=2f;
      Vector3 curPos = transform.parent.position;

      Vector3 targPos = new Vector3(Random.Range(curPos.x-3, curPos.x+3), transform.parent.position.y, Random.Range(curPos.z-3, curPos.z+3));
      
      while (elapsedTime < hangTime) { 
         if (unitHandler.imEngaged == true || unitHandler.ranInto == true) 
            yield break;  

         transform.parent.position = Vector3.Lerp(curPos, targPos, (elapsedTime/hangTime));
         //rB.MovePosition(curPos + (targPos-curPos) * (elapsedTime/hangTime));

         elapsedTime += Time.deltaTime;
         yield return null; 
      } 
      
      yield return new WaitForSeconds(1f);
      StartCoroutine(Orbit());
   }

   private void DialogueEngaged()
   {
      if (GameObject.FindWithTag("InteractUI") == null) {
         //dialogueObj = Instantiate(dialogueUI, canvas.transform.position, dialogueUI.transform.rotation, canvas.transform); 
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

      PlayerStates._Instance.isEngaged = false;
      //unitHandler.imEngaged = false;
        
   }
}
