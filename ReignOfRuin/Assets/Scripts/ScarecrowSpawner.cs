using UnityEngine;

public class ScarecrowSpawner : MonoBehaviour
{
    //private float spawnRangeX = 20;
    //private float spawnRangeY = 20;
    [SerializeField] private Bounds bounds;
    public int spawnAmt;
    public GameObject scarecrowPrefab;
    public UnitHandler stationHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 

    // Update is called once per frame

    void Awake()
    {
        bounds = GetComponent<BoxCollider>().bounds;

        if (GameObject.FindWithTag("Station") != null)
        {
            stationHandler = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
            stationHandler.minigameStarted = true;
        }

        PlayerStates._Instance.ScarecrowMinigame = true;
        for (int i = 0; i < spawnAmt; i++)
        {
            Instantiate(scarecrowPrefab, bounds.center + new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x), 1, Random.Range(-bounds.extents.z, bounds.extents.z)), Quaternion.identity);
        }
    }

    void Update()
    {
        if (PlayerController._Instance.ScarecrowCounter == spawnAmt){
            stationHandler.StateProceed();
            Destroy(gameObject);
            PlayerStates._Instance.ScarecrowMinigame = false;
            PlayerController._Instance.ScarecrowCounter = 0; 
        }
    }
}
