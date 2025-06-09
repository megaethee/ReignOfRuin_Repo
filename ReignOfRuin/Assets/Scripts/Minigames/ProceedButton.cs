using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProceedButton : MonoBehaviour
{
   private SceneController _sceneController;
   public UnitHandler uH, sH;

   private DialogueHandler dH;

   private void Awake()
   {
      if (_sceneController == null)
      {
         _sceneController = FindObjectOfType<SceneController>();
         if (_sceneController == null)
         {
            Debug.LogError("_sceneController not found in the scene.");
         }
      }
      //this shit needs to change 
      dH = DialogueHandler._Instance; 
   }

   public void FindUnit()
   {
      sH = null;
      uH = null;

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

      if (GameObject.FindWithTag("PlayerUnit") != null && uH.imEngaged)
         uH.StateProceed(); 
   } 

   public void SpeechProceedButton()
   { 
      dH.SpeechProceed();
   }

   public void MinigameProceed()
   {
      FindUnit();
      //Debug.Log("Time for a minigame");
      MinigameManager._Instance.InitMinigame(transform.GetSiblingIndex(), sH);
      //Destroy(transform.parent.gameObject);
      transform.parent.parent.gameObject.SetActive(false);
   }
   
   public void Restart()
   {
      _sceneController.HandleButtonPress("StartGame");
   }
}
