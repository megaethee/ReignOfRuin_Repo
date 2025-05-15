using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitHandler : MonoBehaviour
{
   //brainstorm: Use monobehavior exclusives like OnTrigger in scriptable objects  
   public int state = 1, maxStates = 3;  
   public enum UnitType {
      Station,
      Unit
   } public UnitType unitType;

   public bool imEngaged;
   public int statMultiplier;

   private void Awake()
   {
      maxStates = transform.childCount;
      StartCoroutine(WaitForInstance()); 
   } 

   private IEnumerator WaitForInstance()
   {
      while (MinigameManager._Instance == null)
         yield return null;
      
      statMultiplier = MinigameManager._Instance.gameLvl;
   }

   public void StateProceed()
   {
      if (state == maxStates) return; 

      transform.GetChild(state-1).gameObject.GetComponent<UnitInterface>().DestroyUI();
      transform.GetChild(state-1).gameObject.SetActive(false);
 
      transform.GetChild(state++).gameObject.SetActive(true); 
      transform.GetChild(state-1).gameObject.GetComponent<UnitInterface>().Again();
   }

   public void StateReset()
   { 
      transform.GetChild(state-1).gameObject.SetActive(false);
      transform.GetChild(0).gameObject.SetActive(true);
      if (unitType == UnitType.Station) transform.GetChild(0).gameObject.GetComponent<CharacterStation>().Again();
      state = 1;
   }
}
