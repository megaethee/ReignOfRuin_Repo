using UnityEngine;

public class CameraZoomManager : MonoBehaviour
{
    [SerializeField] private Transform player_tsfm;

    public Camera cam;
    public float moveSpeed = 5f;

    private Vector3? targetPosition = null;
    private Quaternion? targetRotation = null;
    public bool followPlayerY = false;
    private float lockedX;

    private void Awake()
    {
        FollowPlayerYOnly();
    }


    public void StopFollowingPlayer()
    {
        Debug.Log("CameraZoomManager: StopFollowingPlayer");
        followPlayerY = false;
    }
    public void FollowPlayerYOnly()
    {
        Debug.Log("CameraZoomManager: FollowPlayerYOnly");
        followPlayerY = true;
        //lockedX = cam.transform.position.x;
        targetPosition = null;    // Stop any target movement
        targetRotation = player_tsfm.rotation;    // Stop any target rotation
    }

    void Update()
    {
         if (followPlayerY && player_tsfm != null)
        {
            Vector3 camPos = cam.transform.position;
            cam.transform.position = player_tsfm.position;
            cam.transform.rotation = Quaternion.Slerp(
                cam.transform.rotation,
                targetRotation.GetValueOrDefault(),
                Time.deltaTime * moveSpeed
            );

            // Stop rotating if close enough
            if (Quaternion.Angle(cam.transform.rotation, targetRotation.GetValueOrDefault()) < 0.01f)
            {
                cam.transform.rotation = targetRotation.Value;
                targetRotation = null;
            }
            return; // Prevent other movement logic from running
        }

        if (targetPosition.HasValue)
        {
            cam.transform.position = Vector3.Lerp(
                cam.transform.position,
                targetPosition.Value,
                Time.deltaTime * moveSpeed
            );

            // Stop moving if close enough
            if (Vector3.Distance(cam.transform.position, targetPosition.Value) < 0.01f)
            {
                cam.transform.position = targetPosition.Value;
                targetPosition = null;
            }
        }

        if (targetRotation.HasValue)
        {
            cam.transform.rotation = Quaternion.Slerp(
                cam.transform.rotation,
                targetRotation.Value,
                Time.deltaTime * moveSpeed
            );

            // Stop rotating if close enough
            if (Quaternion.Angle(cam.transform.rotation, targetRotation.Value) < 0.01f)
            {
                cam.transform.rotation = targetRotation.Value;
                targetRotation = null;
            }
        }
    }

    public void MoveToTarget(Transform target)
    {
        // Keep camera's z position (for 2D), or use target.position.z for 3D
        targetPosition = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
        targetRotation = target.rotation;
    }

    public void ResetCamera()    => MoveToTarget(player_tsfm);
    //public void MoveToBlackSmith() => MoveToTarget(blacksmith_tsfm);
    //public void MoveToWizard()    => MoveToTarget(wizard_tsfm);
    //public void MoveToArchery()   => MoveToTarget(archery_tsfm);
    //public void MoveToPeasant()   => MoveToTarget(peasant_tsfm);
    //public void MoveToDrunk()     => MoveToTarget(drunk_tsfm);
}