using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Wizard : MonoBehaviour
{
    public Troop troop;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemy;
    [SerializeField]
    private int nullCount=0, nonNullCount=0;
    [SerializeField]
    private bool firstTime=false, frame=false;
    GameObject lightning;

    private void Awake()
    {
        transform.parent.tag = "PlayerTroop";
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OpponentTroop") { 
            firstTime = true;
            troop.opponentFound = true;
            enemies.Add(other.gameObject);
            
            //foreach(GameObject en in enemies)
            StartCoroutine(DealDamage(enemies[enemies.Count-1]));
        }
        if (other.tag == "OpponentStronghold" && troop.opponentFound == false) {
            troop.opponentFound = true;
            enemy = other.gameObject;
            StartCoroutine(DealDamageStronghold());
        }
    } 

    void Update()
    {
        if (nonNullCount <= 1 && firstTime && !frame) {
            frame = true;
            StartCoroutine(troop.MoveOnGrid());
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

    private IEnumerator DealDamage(GameObject en)
    { //infinite loop 
        while (true) {
            if (en != null) {
                lightning = Instantiate(Resources.Load<GameObject>("WizardLightning"), en.transform.position + new Vector3(0, 2.75f, 0), Resources.Load<GameObject>("WizardLightning").transform.rotation);
                en.GetComponentInChildren<Troop>().health -= troop.dmg;
                yield return new WaitForSeconds(troop.troopStats.speed);
                Destroy(lightning);
            }  
            else { 
                nullCount++; 
                nonNullCount = enemies.Count - nullCount;
                troop.opponentFound = false;  
                frame = false;
                yield break;
            }
        }
    }
}
