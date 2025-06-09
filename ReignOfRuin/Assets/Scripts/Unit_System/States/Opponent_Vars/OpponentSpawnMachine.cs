using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpponentSpawnMachine : MonoBehaviour, UnitInterface
{
   public List<GameObject> units = new List<GameObject>(); 
   [SerializeField] private Bounds bounds;
   [SerializeField] private float offsetX, offsetZ;
   
   public int amountToSpawn;
   public float spawnInterval = 0.05f;
//store previously spawned positions so they don't spawn on top of each other
   public Vector3 randPos, space;
   [SerializeField] private List<Vector3> randPositions = new List<Vector3>();

   private void Awake()
     {
        bounds = transform.parent.gameObject.GetComponent<BoxCollider>().bounds;  
     }

   public void Again()
   {
      if (randPositions.Count > 0) randPositions.Clear();
      StartCoroutine(SpawnRandom());
   }

   void BoundsCalculator()
   { 
        offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
   }

   private IEnumerator SpawnRandom()
   {
        while (randPositions.Count < amountToSpawn) {
               yield return new WaitForSeconds(spawnInterval);

               BoundsCalculator();
               randPos = bounds.center + new Vector3(offsetX, -1, offsetZ);
               if (!randPositions.Contains(randPos) && randPos != new Vector3(0, -1, 0)) {
                    randPositions.Add(randPos);
                    int randUnit = Random.Range(0, units.Count);
                    Instantiate(units[randUnit], randPos, units[randUnit].transform.rotation); 
               } 
               //space = randPos + new Vector3(0.5f, 0, 0.5f);                
        }
   
      transform.parent.gameObject.GetComponent<UnitHandler>().StateReset();

      yield return null;
        
   }

   public void DestroyUI()
   { 
   }
}
