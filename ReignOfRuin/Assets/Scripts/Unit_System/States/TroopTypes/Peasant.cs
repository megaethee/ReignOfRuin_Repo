using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Peasant : MonoBehaviour
{
    public Troop troop;
    public GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.tag == "PlayerTroop") {
            if (other.tag == "OpponentTroop" && troop.opponentFound == false) { 
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(DealDamage());
            }
            if (other.tag == "OpponentStronghold" && troop.opponentFound == false) {
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(DealDamageStronghold());
            }
        } else if (transform.parent.tag == "OpponentTroop") {
            if (other.tag == "PlayerTroop" && troop.opponentFound == false) { 
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(DealDamage());
            }
            if (other.tag == "PlayerStronghold" && troop.opponentFound == false) {
                troop.opponentFound = true;
                enemy = other.gameObject;
                StartCoroutine(DealDamageStronghold());
            }
        }
    }

    private IEnumerator DealDamageStronghold()
    {
        while (true) { 
            if (enemy != null) {
                enemy.GetComponent<Stronghold>().health -= troop.dmg;

                yield return new WaitForSeconds(troop.troopStats.speed);
            } else {
                troop.opponentFound = false;
                yield break;
            }
        }
    }

    private IEnumerator DealDamage()
    {
        while (true) {
            if (enemy != null) {
                enemy.GetComponentInChildren<Troop>().health -= troop.dmg;

                yield return new WaitForSeconds(troop.troopStats.speed);
            } else {
                troop.opponentFound = false;
                StartCoroutine(troop.MoveOnGrid());
                yield break;
            }
        }
    }
}
