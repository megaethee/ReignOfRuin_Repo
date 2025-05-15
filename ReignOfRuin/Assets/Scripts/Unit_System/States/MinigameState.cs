using UnityEngine;

public class MinigameState : MonoBehaviour, UnitInterface
{
   public GameObject minigame;
   
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

   public void DestroyUI()
   {
      transform.parent.gameObject.GetComponent<UnitHandler>().imEngaged = false;

      //Destroy(miniGameObj);
      if (minigame != null)
         minigame.SetActive(false);
   }
}
