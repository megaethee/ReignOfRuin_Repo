using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TroopOpponent : MonoBehaviour, UnitInterface 
{
    //turn this into two scripts
    public TroopStats troopStats;  
    //public TeleCords tC;
    public Vector2Int oppTeleCords; 

    public float health, dmg;
    public Vector2Int finalTargCord;
    public bool opponentFound;
    private GameObject enemy;

    private void Awake()
    {     
        Again();
    } 

    public void Again()
    {
        opponentFound = false; 
 
        StartCoroutine(WaitForInstance());

    }

    private IEnumerator WaitForInstance()
    {
        while (GridManager._Instance == null)
            yield return null;
        Debug.Log("GridManager ready");

        InitCPUTroop(); 
    }

    private void InitCPUTroop()
    {
        oppTeleCords = new Vector2Int(Random.Range(0, 3), GridManager._Instance.gridSize.y-1); 

        StartCoroutine(MoveToGrid());
    }

    private IEnumerator MoveToGrid()
    {
        float elapsedTime=0, hangTime=2f;
        Vector3 curPos=transform.parent.position, targPos=new Vector3(oppTeleCords.x, transform.parent.position.y, oppTeleCords.y), curRot=transform.parent.eulerAngles;

        while (elapsedTime < hangTime) {
            transform.parent.position = Vector3.Lerp(curPos, targPos, (elapsedTime/hangTime));
            transform.parent.eulerAngles = Vector3.Lerp(curRot, Vector3.forward, (elapsedTime/hangTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.parent.position = targPos;
        transform.parent.eulerAngles = Vector3.forward;
//this always needs to go before so it will get the path of movement right every time
        troopStats.xPosition = Mathf.RoundToInt(transform.parent.position.x);
        troopStats.yPosition = Mathf.RoundToInt(transform.parent.position.z); 
//setting this variable resolves the recompiling bug
        finalTargCord = troopStats.TargCordCompiler();

        StartCoroutine(MoveOnGrid());
    }

    public IEnumerator MoveOnGrid()
    {  
        //probably gonna need to add lerping to this
        if (transform.parent.position.x < GridManager._Instance.grid[finalTargCord].cords.x) {
            for (; transform.parent.position.x < GridManager._Instance.grid[finalTargCord].cords.x; 
                transform.parent.position = new Vector3(transform.parent.position.x+1, transform.parent.position.y, transform.parent.position.z)) {
                
                if (transform.parent.position.x  == 0) continue;
                
                yield return new WaitForSeconds(troopStats.speed);

                if (opponentFound == true) yield break;
            }
        } else if (transform.parent.position.x > GridManager._Instance.grid[finalTargCord].cords.x) {
            for (; transform.parent.position.x > GridManager._Instance.grid[finalTargCord].cords.x; 
                transform.parent.position = new Vector3(transform.parent.position.x-1, transform.parent.position.y, transform.parent.position.z)) {
                
                if (transform.parent.position.x  == 0) continue;
                
                yield return new WaitForSeconds(troopStats.speed);

                if (opponentFound == true) yield break;
            }
        }
        
        for (; transform.parent.position.z > 0; 
            transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z-1)) { 
             
            if (transform.parent.position.z == 0) continue;

            yield return new WaitForSeconds(troopStats.speed);
//this resolves the stopping too late issue
            if (opponentFound == true) yield break;
        }
    }

    void Update()
    {
        if (health <= 0)
            Destroy(transform.parent.gameObject);
         
    } 

    public void DestroyUI()
    {}
} 
