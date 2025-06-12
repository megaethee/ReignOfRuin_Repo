using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour
{
    public static PlayerStates _Instance { get; private set; }
    public Vector3 playerPos;
    public bool isEngaged = false;
    public bool ScarecrowMinigame = false;
    public GameObject m_Projectile; 

    private void Awake()
    {
        if (null == _Instance)
            _Instance = this;
        else   
            Destroy(gameObject); 
    }

    void Update()
    {
        playerPos = transform.position; 

        if (Input.GetKeyDown(KeyCode.Space) && ScarecrowMinigame)
        {
            Instantiate(m_Projectile, transform.position + new Vector3(0, 1, 0), transform.rotation);
        }
    } 

    public IEnumerator Blink()
    { 
        CapsuleCollider cC = gameObject.GetComponent<CapsuleCollider>();

        cC.enabled = false;
        yield return new WaitForSeconds(0.1f); 
        cC.enabled = true;      
    }
}
