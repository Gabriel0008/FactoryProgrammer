using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMotion : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float smoothing = 5f;
    private Vector2 range = new (0,50);
    [SerializeField] private float rangex;
    [SerializeField] private float rangey;
    [SerializeField] private PlayerInput playerInput;

    private Vector3 targetPostion;
    private Vector3 input;

    public static bool canMove = true;

    private InputAction moveHorizontallyAction;
    private InputAction moveVerticallyAction;




    private void Awake() {

        range = new Vector2(InitialValues.Instance.gridHeight * 10, InitialValues.Instance.gridWidth * 10);
        targetPostion = transform.position;
        moveHorizontallyAction = playerInput.actions["MoveCameraHorizontally"];
        moveVerticallyAction = playerInput.actions["MoveCameraVertically"];
        




    }

    private void HandleInput(){
        float x = moveHorizontallyAction.ReadValue<float>();
        float z = moveVerticallyAction.ReadValue<float>();
        Vector3 right = transform.right * x *Time.deltaTime;
        Vector3 forward = transform.forward * z * Time.deltaTime;

        input = (forward + right).normalized;
    }

    private void Move()
    {
        Vector3 nextTargetPosition = targetPostion + input * speed;
        if (IsInBounds(nextTargetPosition)) targetPostion = nextTargetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPostion, Time.deltaTime * smoothing);
    }
    private bool IsInBounds(Vector3 position)
    {
        return position.x > 0 &&
               position.x < range.x &&
               position.z > 0 &&
               position.z < range.y;
    }

    private void Update()
    {
        if (canMove)
        {
            HandleInput();
            Move();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5f);
        
        Gizmos.DrawWireCube(Vector3.zero, new Vector3 (range.x * 2f,5f, range.y * 2f));
    }

}
