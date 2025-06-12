using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour, UnitInterface
{
    public Reel[] reel;
    bool startSpin;
    string[] randomListColor = {"Green", "Red", "Blue"};
    string color;
    public UnitHandler stationHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
       Again(); 
    }

    public void Again()
    {
        startSpin = false;

        if (GameObject.FindWithTag("Station") != null)
        {
            stationHandler = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
            stationHandler.minigameStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSpin)//Prevents Interference If The Reels Are Still Spinning
        {
            if (Input.GetKeyDown(KeyCode.Space))//The Input That Starts The Slot Machine 
            {
                startSpin = true;
                StartCoroutine(Spinning());
            }
        }
    }

    IEnumerator Spinning()
    {
        foreach (Reel spinner in reel)
        {
            //Tells Each Reel To Start Spnning
            spinner.Spin = true;
        }

        for (int i = 0; i < reel.Length; i++)
        {
            //Allow The Reels To Spin For A Random Amout Of Time Then Stop Them
            yield return new WaitForSeconds(Random.Range(1, 3));
            reel[i].Spin = false;
            //reel[i].RandomPosition();
            string color = randomListColor[Random.Range(0, randomListColor.Length)];
            reel[i].AlignMiddle(color);
            if (color == "Green")
            {
                //Allows The Machine To Be Started Again 
                startSpin = false;
                stationHandler.StateProceed();
                //Destroy(gameObject); 
            }
            else if (color == "Red")
            {
                startSpin = false;
                stationHandler.StateReset();
                //Destroy(gameObject); 
            }
            else
            {
                startSpin = false;
                Instantiate(Resources.Load<GameObject>("Peasant_Farmer"), transform.parent.position - new Vector3(1, 1, 0), Resources.Load<GameObject>("Peasant_Farmer").transform.rotation);
                stationHandler.StateReset();
            }
        }        
    }

    public void DestroyUI()
    {
        stationHandler.cameraZoomManager.FollowPlayerYOnly();
        stationHandler.imEngaged = false;
    }
}
