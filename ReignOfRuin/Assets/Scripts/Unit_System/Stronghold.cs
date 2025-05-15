using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stronghold : MonoBehaviour
{
    public float health = 10;

    // Update is called once per frame
    void Update()
    {
       if (health <= 0) Destroy(transform.parent.gameObject); 
    }
}
