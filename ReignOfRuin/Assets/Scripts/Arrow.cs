using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed = 40.0f;
    private float topBound = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
    }
}
