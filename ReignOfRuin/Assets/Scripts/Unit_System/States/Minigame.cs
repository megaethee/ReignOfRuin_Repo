using UnityEngine;

public class Minigame : MonoBehaviour, UnitInterface
{
   public GameObject minigame;
   private GameObject miniGameObj;
   private GameObject canvas;

   private void Awake()
   {
        //Debug.Log("This is a minigame");
   } 

   public void Again()
   {
      miniGameObj = Instantiate(minigame, DialogueHandler._Instance.canvas.transform.position, minigame.transform.rotation, DialogueHandler._Instance.canvas.transform);
   }

   public void DestroyUI()
   {
      transform.parent.gameObject.GetComponent<UnitHandler>().imEngaged = false;
      Destroy(miniGameObj);
   }
}
