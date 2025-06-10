using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chickenPrefab;
    [SerializeField] private Bounds minigameBounds;
    public int spawnAmt;
    //[SerializeField] private float minX;
    //[SerializeField] private float maxX;
    //[SerializeField] private float minZ;
    //[SerializeField] private float maxZ;
    public UnitHandler stationHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        minigameBounds = GetComponent<BoxCollider>().bounds;

        for (int i = 0; i < spawnAmt; i++){
            Instantiate(chickenPrefab, minigameBounds.center + new Vector3(Random.Range(-minigameBounds.extents.x, minigameBounds.extents.x), 0, Random.Range(-minigameBounds.extents.z, minigameBounds.extents.z)), Quaternion.identity);
        }
        if (GameObject.FindWithTag("Station") != null)
        {
            stationHandler = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
            stationHandler.minigameStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController._Instance.CoinCounter == spawnAmt*2){
            stationHandler.StateProceed();
            Destroy(gameObject);
            PlayerController._Instance.CoinCounter = 0; 
        }
    }
}
