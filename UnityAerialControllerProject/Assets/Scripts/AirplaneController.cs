using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public InputPlayerActions InputPlayerActions;

    [Header("Components Airplane")]
    [SerializeField] private float throttleIncrement = 0.1f;
    [SerializeField] private float maxThrust = 200f;
    [SerializeField] private float responsiveness = 10f;
    [SerializeField] private Rigidbody rb;

    private float responseModifier => (rb.mass / 10f) * responsiveness;
    private float throttle;
    private Vector2 deltaDirection;

    private void OnEnable()
    {
        InputPlayerActions = new InputPlayerActions();
        InputPlayerActions.Enable();
    }
    
    private void Update()
    {
       HandleInputs();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrust * throttle, ForceMode.Force);
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
}
