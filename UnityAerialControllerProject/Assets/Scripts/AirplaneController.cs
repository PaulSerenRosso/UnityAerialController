using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public InputPlayerActions InputPlayerActions;

    [SerializeField] private float throttleIncrement = 0.1f;
    [SerializeField] private float maxThrust = 200f;
    [SerializeField] private float responsiveness = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject renderer;

    private float responseModifier => (rb.mass / 10f) * responsiveness;

    private float throttle;
    private float xPosition;
    private float yPosition;

    public void OnEnable()
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
        rb.AddForce(transform.forward * maxThrust * throttle);
        rb.AddTorque(-transform.right * xPosition * responseModifier);
        rb.AddTorque(-transform.forward * yPosition * responseModifier);
    }

    private void HandleInputs()
    {
        xPosition = InputPlayerActions.Player.Vertical.ReadValue<float>();
        yPosition = InputPlayerActions.Player.Horizontal.ReadValue<float>();
        if (xPosition == 0 || yPosition == 0) rb.angularVelocity = Vector3.zero;
        
        if (InputPlayerActions.Player.Accelerate.IsPressed()) throttle += throttleIncrement;
        else throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }
}
