using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private Vector2 range = new(30f, 70f);
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private PlayerInput playerInput;

    private Vector3 cameraDirection => transform.InverseTransformDirection(cameraHolder.forward);

    private Vector3 targetPosition;
    private float input;

    private InputAction cameraZoom;

    private void Awake()
    {
        targetPosition = cameraHolder.localPosition;
        cameraZoom = playerInput.actions["ZoomCamera"];
    }

    private void HandleInput()
    {
        //input = Input.GetAxisRaw("Mouse ScrollWheel");

        input = cameraZoom.ReadValue<float>();


    }

    private void Zoom()
    {
        Vector3 nextTargetPosition = targetPosition + cameraDirection * (input * speed);
        if (IsInBounds(nextTargetPosition)) targetPosition = nextTargetPosition;
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetPosition, Time.deltaTime * smoothing);
    }
    private bool IsInBounds(Vector3 position)
    {
        return position.magnitude > range.x && position.magnitude < range.y;
    }

    private void Update()
    {
        HandleInput();
        Zoom();
    }

}
