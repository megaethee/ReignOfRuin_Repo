using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed = 40.0f;
    public float lifespan = 3f; // this is the projectile's lifespan (in seconds)
     private Rigidbody m_Rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake  ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
