using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private PlayerInput playerInput;

    private float targetAngle;
    private float currentAngle;

    private InputAction cameraRotate;

    private void Awake()
    {
        targetAngle = transform.eulerAngles.y;
        currentAngle = targetAngle;
        cameraRotate = playerInput.actions["RotateCamera"];
    }

    private void HandleInput()
    {
       // if (!Input.GetMouseButton(1)) return;
       // targetAngle += Input.GetAxisRaw("Mouse X") * speed;


        targetAngle += cameraRotate.ReadValue<float>() * speed * Time.deltaTime;
    }

    private void Rotate()
    {
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * smoothing);
        transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);
    }

    private void Update()
    {
        HandleInput();
        Rotate();
    }
}
