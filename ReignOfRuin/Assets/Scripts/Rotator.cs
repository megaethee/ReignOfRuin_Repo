using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.Self);
    }
}
