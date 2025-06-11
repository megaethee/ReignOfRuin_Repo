using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotator : MonoBehaviour
{
    private bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(Orbit());
    }

    private IEnumerator Orbit()
    {
        yield return new WaitForSeconds(1f);

        float elapsedTime=0, hangTime=2f;
        Vector3 curPos = transform.position;

        Vector3 targPos = new Vector3(Random.Range(curPos.x-3, curPos.x+3), transform.position.y, Random.Range(curPos.z-3, curPos.z+3));

        transform.rotation = Quaternion.LookRotation(targPos - curPos);
        
        while (elapsedTime < hangTime) { 
            if (stop) yield break;

            transform.position = Vector3.Lerp(curPos, targPos, (elapsedTime/hangTime));
            //rB.MovePosition(curPos + (targPos-curPos) * (elapsedTime/hangTime));

            elapsedTime += Time.deltaTime;
            yield return null; 
        } 
        
        StartCoroutine(Orbit());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
            stop = true;
    }
    
}
