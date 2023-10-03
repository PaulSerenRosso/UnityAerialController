using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject airplane;
    [SerializeField] private Vector3 offset = new(0f, 1.5f, -4.5f);
    [SerializeField] private float speed;

    private Vector3 target;
    private float travelTimeClamped;

    private void Update()
    {
        target = airplane.transform.TransformPoint(offset);
    }

    private void FixedUpdate()
    {
        if (transform.position == target) return;
        transform.position = Vector3.Lerp(transform.position, target, Mathf.Clamp(speed * Time.deltaTime, 0.0f, 1.0f));
        transform.rotation = airplane.transform.rotation;
    }
}
