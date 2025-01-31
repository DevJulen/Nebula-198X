using UnityEngine;
using UnityEngine.InputSystem;

public class NBInputManager : MonoBehaviour
{
    public PlayerInput playerInput;

    // Input Actions
    private InputAction _movementInputAction;
    private InputAction _aimInputAction;

    // Action values
    public Vector2 MovementInput { get; private set; }
    public Vector2 AimInput { get; private set; }


    public bool IsGamepad { get; private set; }

    public static NBInputManager Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitializeInputActions();
    }

    private void Update()
    {
        GetInputs();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += InputActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= InputActionChange;
    }

    /// <summary>
    ///  Initializes all the input actions
    /// </summary>
    private void InitializeInputActions()
    {
        playerInput = GetComponent<PlayerInput>();

        _movementInputAction = playerInput.actions[Constants.MOVEMENT_ACTION];
        _aimInputAction = playerInput.actions[Constants.AIM_ACTION];
    }

    /// <summary>
    /// Reads all inputs
    /// </summary>
    private void GetInputs()
    {
        MovementInput = _movementInputAction.ReadValue<Vector2>();

        if (IsGamepad) AimInput = _aimInputAction.ReadValue<Vector2>();
        else AimInput = GetMouseDirection();
    }

    /// <summary>
    /// Keeps track of the input action changes
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="change"></param>
    private void InputActionChange(object obj, InputActionChange change)
    {
        if (change == UnityEngine.InputSystem.InputActionChange.ActionPerformed)
        {
            InputAction receivedInputAction = (InputAction)obj;
            InputDevice lastDevice = receivedInputAction.activeControl.device;
            IsGamepad = !(lastDevice.name.Equals("Keyboard") || lastDevice.name.Equals("Mouse"));
        }
    }

    /// <summary>
    /// Returns the direction from the player to the mouse position.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetMouseDirection()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        //TODO: TEMPORAL POR EL FINDOBJECT
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 direction = (worldMousePosition - (Vector3)playerPosition).normalized;

        return direction;
    }
}
