using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroopOpponent : MonoBehaviour, UnitInterface
{
    //turn this into two scripts
    public TroopStats troopStats;   
    public TeleCords tC; 
    public SpawnMachine spawnRef;

    public float health, dmg;
    public Vector2Int finalTargCord, oppCords;
    public Vector3 targPos;
    public bool opponentFound;  

    private void Awake()
    { 
        if (transform.parent.tag == "OpponentTroop")
            Again();  
    } 

    public void Again()
    { 
        if (transform.parent.tag == "PlayerTroop") {
            
            //spawnRef.toRestart++;
            StartCoroutine(PlayerStates._Instance.Blink());

        
            switch (transform.parent.gameObject.GetComponent<UnitHandler>().statMultiplier) {    
                case 1: 
                    health = troopStats.health;
                    dmg = troopStats.dmg; 
                    break;
                case 2:
                    health = troopStats.health*1.25f;
                    dmg = troopStats.dmg*1.25f;
                    break;
                case 3:
                    health = troopStats.health*1.5f;
                    dmg = troopStats.dmg*1.5f;
                    break;
            }
        }

        health = troopStats.health;
        dmg = troopStats.dmg;
        
        opponentFound = false; 
 
        StartCoroutine(WaitForInstance());

    }

    private IEnumerator WaitForInstance()
    {
        while (GridManager._Instance == null)
            yield return null;
        Debug.Log("GridManager ready");

        if (transform.parent.tag == "OpponentTroop")
            InitCPUTroop();
        else if (transform.parent.tag == "PlayerTroop")
            StartCoroutine(MoveToGrid());
    }

    private void InitCPUTroop()
    {
        oppCords = new Vector2Int(Random.Range(0, 3), GridManager._Instance.gridSize.y-1); 

        Debug.Log("I am enemy");

        StartCoroutine(MoveToGrid());
    }

    private IEnumerator MoveToGrid()
    {
        transform.parent.parent.GetComponent<Animator>().SetBool("isMoving", true);

        float elapsedTime=0, hangTime=2f;
        Vector3 curPos=transform.parent.parent.position, curRot=transform.parent.parent.eulerAngles;
        //Vector3 targPos;
        if (transform.parent.tag == "PlayerTroop")
            targPos=new Vector3(tC.teleCords.x, transform.parent.parent.position.y, tC.teleCords.y);
        else if (transform.parent.tag == "OpponentTroop")
            targPos=new Vector3(oppCords.x, transform.parent.parent.position.y, oppCords.y);

        while (elapsedTime < hangTime) {
            transform.parent.parent.position = Vector3.Lerp(curPos, targPos, (elapsedTime/hangTime));

            if (transform.parent.tag == "PlayerTroop") 
                transform.parent.parent.eulerAngles = Vector3.Lerp(curRot, Vector3.forward, (elapsedTime/hangTime));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.parent.parent.position = targPos;
        if (transform.parent.tag == "PlayerTroop")
            transform.parent.parent.eulerAngles = Vector3.forward;
        //this always needs to go before so it will get the path of movement right every time
        troopStats.xPosition = Mathf.RoundToInt(transform.parent.parent.position.x);
        troopStats.yPosition = Mathf.RoundToInt(transform.parent.parent.position.z); 
//setting this variable resolves the recompiling bug
        finalTargCord = troopStats.TargCordCompiler();

        StartCoroutine(MoveOnGrid());
    }

    public IEnumerator MoveOnGrid()
    {  
        transform.parent.parent.GetComponent<Animator>().SetBool("isMoving", true);

        //probably gonna need to add lerping to this
        if (transform.parent.parent.position.x < GridManager._Instance.grid[finalTargCord].cords.x)
        {
            for (; transform.parent.parent.position.x < GridManager._Instance.grid[finalTargCord].cords.x;
                transform.parent.parent.position = new Vector3(transform.parent.parent.position.x + 1, transform.parent.parent.position.y, transform.parent.parent.position.z))
            {

                if (transform.parent.parent.position.x == 0) continue;

                yield return new WaitForSeconds(troopStats.speed);

                if (opponentFound == true)
                {
                    Debug.Log("I can't move");
                    yield break;
                }
            }
        }
        else if (transform.parent.parent.position.x > GridManager._Instance.grid[finalTargCord].cords.x)
        {
            for (; transform.parent.parent.position.x > GridManager._Instance.grid[finalTargCord].cords.x;
                transform.parent.parent.position = new Vector3(transform.parent.parent.position.x - 1, transform.parent.parent.position.y, transform.parent.parent.position.z))
            {

                if (transform.parent.parent.position.x == 0) continue;

                yield return new WaitForSeconds(troopStats.speed);

                if (opponentFound == true)
                {
                    Debug.Log("I can't move");
                    yield break;
                }
            }
        }
        if (transform.parent.tag == "PlayerTroop") { 
            for (; transform.parent.parent.position.z < GridManager._Instance.grid[finalTargCord].cords.y; 
                transform.parent.parent.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.z+1)) { 
                
                if (transform.parent.parent.position.z == 0) continue;

                yield return new WaitForSeconds(troopStats.speed);
    //this resolves the stopping too late issue
                if (opponentFound == true) {
                    Debug.Log("I can't move");
                    yield break;
                }
            }
        }
        else if (transform.parent.tag == "OpponentTroop") {
            for (; transform.parent.parent.position.z > 0; 
                transform.parent.parent.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.z-1)) { 
                
                if (transform.parent.parent.position.z == 0) continue;

                yield return new WaitForSeconds(troopStats.speed);
    //this resolves the stopping too late issue
                if (opponentFound == true) yield break;
            }
        }

        if (troopStats.path == TroopStats.Path.Drunk) {
            //Debug.Log("Do something");
            GetComponent<Drunkard>().Recalc();
        }
    }

    void Update()
    {
        if (health <= 0)
            Destroy(transform.parent.parent.gameObject);
         
    } 

    public void DestroyUI()
    {}
} 
