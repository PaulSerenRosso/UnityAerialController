using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform AirplaneTransform;
    [SerializeField] private float camSmoothSpeed = 5f;
    [SerializeField] private Transform cameraHolder;

    private void Update()
    {
        transform.position = AirplaneTransform.position;
        cameraHolder.rotation = Damp(cameraHolder.rotation, Quaternion.LookRotation(AirplaneTransform.forward, AirplaneTransform.up), camSmoothSpeed, Time.deltaTime);
    }
    
    private Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt)
    {
        return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }
}
