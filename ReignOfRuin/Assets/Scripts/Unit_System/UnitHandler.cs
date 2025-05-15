using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitHandler : MonoBehaviour
{
   //brainstorm: Use monobehavior exclusives like OnTrigger in scriptable objects  
   public int state = 1, maxStates = 3;  
   public enum UnitType {
      Station,
      Unit,
      Opponent
   } public UnitType unitType;

   public bool imEngaged, ranInto;
   public int statMultiplier;
   public GameObject interactObj;

   public CameraZoomManager cameraZoomManager;

   [SerializeField] 
   private int instantCounter = 0;

   private void Awake()
   {
      cameraZoomManager = GameObject.Find("Cameras").GetComponent<CameraZoomManager>();
      maxStates = transform.childCount;
      gameObject.tag = "Untagged";
      transform.GetChild(0).gameObject.GetComponent<UnitInterface>().Again();
      StartCoroutine(WaitForInstance()); 
   } 

   private IEnumerator WaitForInstance()
   {
      while (MinigameManager._Instance == null)
         yield return null;
      
      statMultiplier = MinigameManager._Instance.gameLvl;
   }

   void Update()
   {
      if (imEngaged && instantCounter < 1)
      {
         interactObj = Instantiate(Resources.Load<GameObject>("Interaction_Indicator"), transform.position + new Vector3(0, 0.9f, 0), Resources.Load<GameObject>("Interaction_Indicator").transform.rotation);
         instantCounter++;
      }
      else if (!imEngaged && instantCounter >= 1)
      {
         if (interactObj != null)
            Destroy(interactObj);
         instantCounter = 0;
      }
   }

   public void StateProceed()
   {
      if (state == maxStates)  
         return;  

      transform.GetChild(state-1).gameObject.GetComponent<UnitInterface>().DestroyUI();
      transform.GetChild(state-1).gameObject.SetActive(false);
 
      transform.GetChild(state++).gameObject.SetActive(true); 
      if (state == maxStates) imEngaged = false;
      transform.GetChild(state-1).gameObject.GetComponent<UnitInterface>().Again();
   }

   public void StateReset()
   { 
      transform.GetChild(state-1).gameObject.SetActive(false);
      transform.GetChild(0).gameObject.SetActive(true);
      if (unitType == UnitType.Station) transform.GetChild(0).gameObject.GetComponent<CharacterStation>().Again();
      state = 1;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Building" || other.tag == "Station") {
         ranInto = true;
         //Debug.Log("I ran into something");
      }
   }
   
}
