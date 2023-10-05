using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class AirplaneController : MonoBehaviour
{
    public InputPlayerActions InputPlayerActions;
    public CameraFollower mainCamera;
    public SpeedParticleContainer speedParticleContainer;


    [Header("Components Airplane")] [SerializeField]
    private float throttleIncrement = 0.1f;

    [SerializeField] private float maxThrust = 200f;
    [SerializeField] private float responsivenessX = 10f;
    [SerializeField] private float responsivenessY = 10f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject lightAirplane;
    [SerializeField] private Vector2 maxClamped;

    private float responseModifierX => (rb.mass / 10f) * responsivenessX;
    private float responseModifierY => (rb.mass / 10f) * responsivenessY;
    private float throttle;
    private Vector2 deltaDirection;
    private bool isLocked;

    private bool canMove = true;
    
    private void OnEnable()
    {
        InputPlayerActions = new InputPlayerActions();
        InputPlayerActions.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        HandleInputs();
        HandleLight();
        speedParticleContainer.UpdateParticle(throttle/100);
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        rb.AddForce(transform.forward * maxThrust * throttle, ForceMode.Force);
        if (isLocked) return;
        var deltaX = Mathf.Clamp(deltaDirection.x, -maxClamped.x, maxClamped.x);
        var deltaY = Mathf.Clamp(deltaDirection.y, -maxClamped.y, maxClamped.y);
        rb.AddTorque(-transform.forward * deltaX * responseModifierX, ForceMode.Force);
        rb.AddTorque(-transform.right * deltaY * responseModifierY, ForceMode.Force);
    }

    private void HandleInputs()
    {
        if (!canMove) return;
        deltaDirection = InputPlayerActions.Player.Direction.ReadValue<Vector2>();
        if (deltaDirection == Vector2.zero) rb.angularVelocity = Vector3.zero;

        if (InputPlayerActions.Player.Accelerate.IsPressed())
        {
            throttle += throttleIncrement;
            speedParticleContainer.ActivateParticle();
        }
        else
        {
            throttle -= throttleIncrement;
            speedParticleContainer.DeactivateParticle();
        }
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void HandleLight()
    {
        if (InputPlayerActions.Player.Light.IsPressed())
        {
            lightAirplane.SetActive(true);
            isLocked = true;
            Cursor.visible = true;
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.main.nearClipPlane;
            Ray rayMouse = Camera.main.ScreenPointToRay(mousePosition); 
            Vector3 newDir = Vector3.RotateTowards(lightAirplane.transform.forward,
                rayMouse.direction * 10, Time.deltaTime, 0.0f);
            lightAirplane.transform.rotation = Quaternion.LookRotation(newDir);
        }
        else
        {
            lightAirplane.SetActive(false);
            isLocked = false;
            Cursor.visible = false;
        }
    }

    public void Rotate(Vector3 newAngle)
    {
        if (GetComponent<PlayerCollision>().IsDead()) return;
        canMove = false;

        transform.forward = newAngle;

        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        throttle = 0f;
        StartCoroutine(ResetCanMove(1));
    }

    IEnumerator ResetCanMove(float timer)
    {
        yield return new WaitForSeconds(timer);

        canMove = true;
    }

    public void LockControls()
    {
        Debug.Log("Controls locked");
        canMove = false;
    }
}