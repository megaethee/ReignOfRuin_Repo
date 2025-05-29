using UnityEngine;

public class FollowPlayerZ : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player Transform in the Inspector
    [SerializeField] private float cameraStartOffset = 0.5f; // Offset from the player's position
    private float cameraOffset;
    private float fixedX;
    private float fixedY;
    public bool offsetOn;

    void Awake()
    {
        // Store the initial X and Y positions
        fixedX = transform.position.x;
        fixedY = transform.position.y;
    }
    public void ToggleOffset(){
        if(offsetOn){
            cameraOffset = cameraStartOffset;
            offsetOn = false;
        }
        else{
            cameraOffset = cameraStartOffset;
            offsetOn = true;
        }
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