using UnityEngine;

public class FollowPlayerZ : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player Transform in the Inspector
    [SerializeField] private float cameraOffset = 0.5f; // Offset from the player's position
    private float fixedX;
    private float fixedY;

    void Awake()
    {
        // Store the initial X and Y positions
        fixedX = transform.position.x;
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                fixedX,
                fixedY,
                player.position.z - cameraOffset
            );
        }
    }
}