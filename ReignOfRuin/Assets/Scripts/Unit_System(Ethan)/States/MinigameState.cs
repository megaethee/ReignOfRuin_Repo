using UnityEngine;

public class MinigameState : MonoBehaviour, UnitInterface
{
   private GameObject minigame;
   
   //private GameObject miniGameObj;
   private GameObject canvas;

   private void Awake()
   {
        //Debug.Log("This is a minigame");
   } 

   public void Again()
   {
      
      MinigameManager._Instance.curPosition = transform.parent.position;

      transform.parent.GetComponent<UnitHandler>().cameraZoomManager.FollowPlayerYOnly();
      //miniGameObj = Instantiate(minigame, DialogueHandler._Instance.canvas.transform.position, minigame.transform.rotation, DialogueHandler._Instance.canvas.transform);
      minigame = GameObject.Find("InteractionUI").transform.GetChild(1).gameObject;
      minigame.SetActive(true);
   }

   //Trigger box exit
   //UnitHandler.StateReset
   //Set that ui inactive
   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player" && !transform.parent.GetComponent<UnitHandler>().minigameStarted) {
         minigame.SetActive(false);
         transform.parent.GetComponent<UnitHandler>().StateReset();
      }
   }

   public void DestroyUI()
   {
      transform.parent.gameObject.GetComponent<UnitHandler>().imEngaged = false;

      //Destroy(miniGameObj);
      if (minigame != null)
         minigame.SetActive(false);
   }
}
