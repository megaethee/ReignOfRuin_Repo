using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour
{
    public static PlayerStates _Instance { get; private set; }
    public Vector3 playerPos;
    public bool isEngaged = false;

    private void Awake()
    {
        if (null == _Instance)
            _Instance = this;
        else   
            Destroy(gameObject); 
    }

    void Update()
    {
        playerPos = transform.position; 

        //if (isEngaged == true)
        //    gameObject.GetComponent<CapsuleCollider>().enabled = false;
        //else
        //    gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    //public void Blink()
    //{
    //    StartCoroutine(BlinkRoutine());
    //}

    //private IEnumerator BlinkRoutine()
    //{ 
    //    gameObject.GetComponent<CapsuleCollider>().enabled = false;
    //    yield return new WaitForSeconds(0.1f); 
    //    gameObject.GetComponent<CapsuleCollider>().enabled = true; 
    //}
}
