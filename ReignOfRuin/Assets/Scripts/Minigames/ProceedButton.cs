using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ProceedButton : MonoBehaviour
{
   public UnitHandler uH, sH;

   private DialogueHandler dH;

   private void Awake()
   {
      //this shit needs to change 
      dH = DialogueHandler._Instance; 
   }

   public void FindUnit()
   {
      if (GameObject.FindWithTag("PlayerUnit") != null)
         uH = GameObject.FindWithTag("PlayerUnit").GetComponent<UnitHandler>();
      if (GameObject.FindWithTag("Station") != null)
         sH = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
   }

   public void CompleteProceedButton()
   { 
      FindUnit();

      if (GameObject.FindWithTag("Station") != null && sH.imEngaged)
         sH.StateProceed();

      if (GameObject.FindWithTag("PlayerUnit") != null)
         uH.StateProceed(); 
   } 

   public void SpeechProceedButton()
   { 
      dH.SpeechProceed();
   }

   public void MinigameProceed()
   {
      //FindUnit();
      //Debug.Log("Time for a minigame");
      MinigameManager._Instance.InitMinigame(transform.GetSiblingIndex(), sH);
      //Destroy(transform.parent.gameObject);
      transform.parent.parent.gameObject.SetActive(false);
   }
   
   public void Restart()
   {
      SceneManager.LoadScene(0);
   }
}
