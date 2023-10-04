using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Airplane;
    [SerializeField] private float camSmoothSpeed = 5f;
    [SerializeField] private Transform cameraHolder;

    private void Update()
    {
        // Smoothly rotate the camera to face the mouse aim.
        transform.position = Airplane.transform.position;
        Vector3 upVec = (Mathf.Abs(Airplane.forward.y) > 0.9f) ? cameraHolder.up : Vector3.up;
        cameraHolder.rotation = Damp(cameraHolder.rotation, Quaternion.LookRotation(Airplane.forward, upVec), camSmoothSpeed, Time.deltaTime);
    }
    
    private Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt)
    {
        return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }
}
