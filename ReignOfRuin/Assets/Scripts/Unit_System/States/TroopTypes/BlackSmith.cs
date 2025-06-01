using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blacksmith : MonoBehaviour
{
    public Troop troop;
    public GameObject enemy;
    public Vector2Int moveBackCords;
    public bool movedBack = false;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.tag == "PlayerTroop") {
            moveBackCords = new Vector2Int(1, 0);
            if (other.tag == "PlayerTroop" && troop.opponentFound == false) { 
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(HealthBoost());
            }
            if (other.tag == "PlayerStronghold" && troop.opponentFound == false && movedBack == true) {
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(HealStronghold());
            }
        } else if (transform.parent.tag == "OpponentTroop") {
            moveBackCords = new Vector2Int(1, GridManager._Instance.gridSize.y);
            if (other.tag == "OpponentTroop" && troop.opponentFound == false) { 
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(HealthBoost());
            }
            if (other.tag == "OpponentStronghold" && troop.opponentFound == false && movedBack == true) {
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(HealStronghold());
            }
        }
    }

    private IEnumerator HealStronghold()
    {
        while (true) { 
            if (enemy == null) {
                Debug.Log("I am healing");
                enemy.GetComponent<Stronghold>().health += troop.dmg;

                yield return new WaitForSeconds(troop.troopStats.speed);
            } else {
                troop.opponentFound = false;
                yield break;
            }
        }
    }

    private IEnumerator HealthBoost()
    { 
        if (enemy != null) {
            Debug.Log("I am healing");
            enemy.GetComponentInChildren<Troop>().health += troop.dmg;

            yield return new WaitForSeconds(troop.troopStats.speed);
        
            //troop.opponentFound = false;
            StartCoroutine(MoveBack());
            
            yield break;
        } 
    }

    private IEnumerator MoveBack()
    {
        movedBack = true;
        Debug.Log(movedBack);
        //enemy = null; 

        Debug.Log("I am moving back");

        if (transform.parent.position.x < moveBackCords.x) {
            for (; transform.parent.position.x < moveBackCords.x; 
                transform.parent.position = new Vector3(transform.parent.position.x+1, transform.parent.position.y, transform.parent.position.z)) {
                
                if (transform.parent.position.x  == 0) continue;
                
                yield return new WaitForSeconds(troop.troopStats.speed);

                //if (opponentFound == true) { 
                //    Debug.Log("I can't move");
                //    yield break;
                //}
            }
        } else if (transform.parent.position.x > moveBackCords.x) {
            for (; transform.parent.position.x > moveBackCords.x; 
                transform.parent.position = new Vector3(transform.parent.position.x-1, transform.parent.position.y, transform.parent.position.z)) {
                
                if (transform.parent.position.x  == 0) continue;
                
                yield return new WaitForSeconds(troop.troopStats.speed);

                //if (opponentFound == true) {
                //    Debug.Log("I can't move");
                //    yield break;
                //}
            }
        }
        
        if (transform.parent.tag == "PlayerTroop") { 
            transform.parent.rotation = Quaternion.Euler(0, 180, 0);
            for (; transform.parent.position.z > moveBackCords.y; 
                transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z-1)) { 
                
                if (transform.parent.position.z == 0) continue;

                yield return new WaitForSeconds(troop.troopStats.speed);
    //this resolves the stopping too late issue
                //if (opponentFound == true) {
                //    Debug.Log("I can't move");
                //    yield break;
                //}
            }
        }
        else if (transform.parent.tag == "OpponentTroop") {
            //transform.parent.rotation.y = 0;
            for (; transform.parent.position.z < moveBackCords.y; 
                transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z+1)) { 
                
                if (transform.parent.position.z == 0) continue;

                yield return new WaitForSeconds(troop.troopStats.speed);
    //this resolves the stopping too late issue
                //if (opponentFound == true) yield break;
            }
        }
        yield break;
    }
}
