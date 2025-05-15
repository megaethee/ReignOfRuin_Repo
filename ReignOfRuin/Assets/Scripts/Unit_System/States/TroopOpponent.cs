using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroopOpponent : MonoBehaviour, UnitInterface 
{
    //turn this into two scripts
    public TroopStats troopStats;  
    public TeleCords tC;

    private Vector2Int testCords = new Vector2Int(0, 0); 

    public float health, dmg;
    private GameObject enemy;

    private void Awake()
    {  
        troopStats.xPosition = Mathf.RoundToInt(transform.parent.position.x);
        troopStats.yPosition = Mathf.RoundToInt(transform.parent.position.z); 

        health = troopStats.health;
        dmg = troopStats.dmg;

        //StartCoroutine(WaitForInstance());
    } 

    public void Again()
    {}

    private IEnumerator WaitForInstance()
    {
        while (GridManager._Instance == null)
            yield return null;
        Debug.Log("GridManager ready");

        StartCoroutine(MoveOnGridOpponent());
    }
 
    private IEnumerator MoveOnGridOpponent()
    { 
        if (transform.parent.position.x < GridManager._Instance.grid[testCords].cords.x) {
            for (; transform.parent.position.x < GridManager._Instance.grid[testCords].cords.x; 
                transform.parent.position = new Vector3(transform.parent.position.x+1, transform.parent.position.y, transform.parent.position.z)) {
                if (transform.parent.position.x  == 0) continue;
                
                yield return new WaitForSeconds(troopStats.speed);
            }
        } else if (transform.parent.position.x > GridManager._Instance.grid[testCords].cords.x) {
            for (; transform.parent.position.x > GridManager._Instance.grid[testCords].cords.x; 
                transform.parent.position = new Vector3(transform.parent.position.x-1, transform.parent.position.y, transform.parent.position.z)) {
                if (transform.parent.position.x  == 0) continue;
                
                yield return new WaitForSeconds(troopStats.speed);
            }
        }

        for (; transform.parent.position.z > GridManager._Instance.grid[testCords].cords.y; 
            transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z-1)) { 
            //Debug.Log(GridManager._Instance.grid[troopStats.TargCordCompiler()].cords.y);
            if (transform.parent.position.z == 0) continue;

            yield return new WaitForSeconds(troopStats.speed);
        }
    }
    
    void Update()
    {
        if (health <= 0)
            Destroy(transform.parent.gameObject);
        
       // Debug.Log(health);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerTroop") {
            enemy = other.gameObject; 

            StartCoroutine(DealDamage());
        }
    } 

    private IEnumerator DealDamage()
    {
        while (true) {
            if (enemy != null) {
                enemy.GetComponentInChildren<Troop>().health -= dmg;

                yield return new WaitForSeconds(troopStats.speed);
            } else {
                yield break;
            }
        }
    }

    public void DestroyUI()
    {}
} 
