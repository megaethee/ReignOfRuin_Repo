using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public Reel[] reel;
    bool startSpin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSpin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSpin)//Prevents Interference If The Reels Are Still Spinning
        {
            if (Input.GetKeyDown(KeyCode.K))//The Input That Starts The Slot Machine 
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

        for(int i = 0; i < reel.Length; i++)
        {
            //Allow The Reels To Spin For A Random Amout Of Time Then Stop Them
            yield return new WaitForSeconds(Random.Range(1, 3));
            reel[i].Spin = false;
            reel[i].RandomPosition();
        }

        //Allows The Machine To Be Started Again 
        startSpin = false;
    }
}
