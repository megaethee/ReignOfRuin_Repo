using UnityEngine;

public class KegSpawner : MonoBehaviour
{
    [SerializeField] private GameObject kegPrefab;
    [SerializeField] private Bounds minigameBounds;
    //[SerializeField] private float minX;
    //[SerializeField] private float maxX;
    //[SerializeField] private float minZ;
    //[SerializeField] private float maxZ;
    public UnitHandler stationHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        minigameBounds = GetComponent<BoxCollider>().bounds;
        Instantiate(kegPrefab, minigameBounds.center + new Vector3(Random.Range(-minigameBounds.extents.x, minigameBounds.extents.x), 0.75f, Random.Range(-minigameBounds.extents.z, minigameBounds.extents.z)), Quaternion.identity);
        
        if (GameObject.FindWithTag("Station") != null) {
            stationHandler = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
            stationHandler.minigameStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController._Instance.CoinCounter == 1){
            stationHandler.StateProceed();
            PlayerController._Instance.CoinCounter = 0;
            Destroy(gameObject);
        }
    }
}
