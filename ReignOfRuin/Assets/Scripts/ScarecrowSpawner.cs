using UnityEngine;

public class ScarecrowSpawner : MonoBehaviour
{
    private float spawnRangeX = 20;
    private float spawnRangeY = 20;
    public GameObject scarecrowPrefab;
    public UnitHandler stationHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame


    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(scarecrowPrefab,new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.75f, Random.Range(-spawnRangeY, spawnRangeY)), Quaternion.identity);
        }
    }

    void Update()
    {
        if (PlayerController._Instance.ScarecrowCounter == 3){
            stationHandler.StateProceed();
            Destroy(gameObject);
            PlayerController._Instance.CoinCounter = 0; 
        }
    }
}
