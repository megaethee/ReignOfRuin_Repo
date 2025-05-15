using UnityEngine;

public class KegSpawner : MonoBehaviour
{
    [SerializeField] private GameObject kegPrefab;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    public UnitHandler stationHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        Instantiate(kegPrefab, new Vector3(Random.Range(minX, maxX), 0.75f,Random.Range(minZ,maxZ)), Quaternion.identity);
        if (GameObject.FindWithTag("Station") != null)
            stationHandler = GameObject.FindWithTag("Station").GetComponent<UnitHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController._Instance.CoinCounter == 1){
            stationHandler.StateProceed();
            PlayerController._Instance.CoinCounter = 0;
            Destroy(this);
        }
    }
}
