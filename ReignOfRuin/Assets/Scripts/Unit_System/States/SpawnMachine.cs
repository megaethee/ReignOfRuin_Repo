using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnMachine : MonoBehaviour, UnitInterface
{
   public GameObject unit; 
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
        offsetZ = Random.Range(-bounds.extents.z, 0);
   }

   private IEnumerator SpawnRandom()
   {
        while (randPositions.Count < amountToSpawn) {
               BoundsCalculator();
               randPos = bounds.center + new Vector3(offsetX, 0f, offsetZ);
               if (!randPositions.Contains(randPos)) {
                    randPositions.Add(randPos);
                    Instantiate(unit, randPos, unit.transform.rotation); 
               }
               
               //space = randPos + new Vector3(0.5f, 0, 0.5f);
               
               yield return new WaitForSeconds(spawnInterval); 
        }
   
      transform.parent.gameObject.GetComponent<UnitHandler>().StateReset();

      yield return null;
        
   }

   public void DestroyUI()
   { 
   }
}
