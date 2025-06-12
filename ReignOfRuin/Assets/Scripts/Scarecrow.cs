using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow") {
            PlayerController._Instance.ScarecrowCounter++;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
