using UnityEngine;
using UnityEngine.InputSystem; // Required for new input system

public class OrbitingCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float xSpeed = 120f;
    public float ySpeed = 80f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector2 lookInput;

    private InputAction lookAction;

    void OnEnable()
    {
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        lookAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
    }

    void LateUpdate()
    {
        if (target)
        {
            lookInput = lookAction.ReadValue<Vector2>();

            x += lookInput.x * xSpeed * 0.02f;
            y -= lookInput.y * ySpeed * 0.02f;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
