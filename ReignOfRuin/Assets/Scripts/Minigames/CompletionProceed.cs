using UnityEngine;

public class CompletionProceed : MonoBehaviour
{
   public UnitHandler uH, sH;

   private DialogueHandler dH;

   private void Awake()
   {
      //this shit needs to change 
      if (GameObject.FindWithTag("PlayerUnit") != null)
         uH = GameObject.FindWithTag("PlayerUnit").GetComponent<UnitHandler>();
      if (GameObject.FindWithTag("Station") != null)
         sH = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();

      dH = DialogueHandler._Instance; 
   }

   public void CompleteProceed()
   { 
      if (GameObject.FindWithTag("Station") != null && sH.imEngaged)
         sH.StateProceed();

      if (GameObject.FindWithTag("PlayerUnit") != null)
         uH.StateProceed(); 
   } 

   public void SpeechProceed()
   { 
      dH.testDialogue.inx++;
   
      if (dH.testDialogue.inx >= dH.testDialogue.dialogueSequence.Capacity) {
            CompleteProceed();
            return;
      } else  
         if (dH.testDialogue.dialogueSequence[dH.testDialogue.inx] == null) {
            CompleteProceed();
            return; 
         }
         else
            dH.StartCoroutine(dH.TypeWriter(dH.testDialogue.dialogueSequence[dH.testDialogue.inx], dH.testDialogue));     
   }

   public void MinigameProceed()
   {
      MinigameManager._Instance.InitMinigame(transform.GetSiblingIndex(), sH);
      Destroy(transform.parent.gameObject);
   }
}
