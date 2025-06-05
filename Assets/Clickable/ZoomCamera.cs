using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomCamera : MonoBehaviour
{
    public Transform target;
    public float zoomSpeed = 10f;
    public float minDistance = 3f;
    public float maxDistance = 20f;

    private float currentDistance;
    private InputAction scrollAction;

    void OnEnable()
    {
        scrollAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll");
        scrollAction.Enable();
    }

    void OnDisable()
    {
        scrollAction.Disable();
    }

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("ZoomCamera: Target is not assigned.");
            return;
        }

        currentDistance = Vector3.Distance(transform.position, target.position);
    }

    void LateUpdate()
    {
        if (target == null) return;

        float scrollInput = scrollAction.ReadValue<Vector2>().y;

        // Zoom logic
        currentDistance -= scrollInput * zoomSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Move camera along its forward axis
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * currentDistance;
    }
}
