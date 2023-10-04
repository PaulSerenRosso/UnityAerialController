using UnityEngine;
using UnityEngine.InputSystem;

public class AirplaneController : MonoBehaviour
{
    public InputPlayerActions InputPlayerActions;

    [Header("Components Airplane")] [SerializeField]
    private float throttleIncrement = 0.1f;

    [SerializeField] private float maxThrust = 200f;
    [SerializeField] private float responsiveness = 10f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject lightAirplane;

    private float responseModifier => (rb.mass / 10f) * responsiveness;
    private float throttle;
    private Vector2 deltaDirection;
    private bool isLocked;

    private void OnEnable()
    {
        InputPlayerActions = new InputPlayerActions();
        InputPlayerActions.Enable();
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        HandleInputs();
        HandleLight();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrust * throttle, ForceMode.Force);
        if (isLocked) return;
        rb.AddTorque(-transform.right * deltaDirection.y * responseModifier, ForceMode.Force);
        rb.AddTorque(-transform.forward * deltaDirection.x * responseModifier, ForceMode.Force);
    }

    private void HandleInputs()
    {
        deltaDirection = InputPlayerActions.Player.Direction.ReadValue<Vector2>();
        if (deltaDirection == Vector2.zero) rb.angularVelocity = Vector3.zero;

        if (InputPlayerActions.Player.Accelerate.IsPressed()) throttle += throttleIncrement;
        else throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void HandleLight()
    {
        if (InputPlayerActions.Player.Light.IsPressed())
        {
            lightAirplane.SetActive(true);
            isLocked = true;
            // Cursor.visible = true;
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
            // Cursor.visible = false;
        }
    }
}