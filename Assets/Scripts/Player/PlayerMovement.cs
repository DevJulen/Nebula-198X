using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private PlayerConfig configuration;
    private Rigidbody2D _rigidBody;

    // Control variables
    private Vector2 _lastDirection;
    private Vector2 _targetVelocity;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    #region PRIVATE METHODS
    /// <summary>
    /// Initializes the player movement components
    /// </summary>
    private void Initialize()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves the ship
    /// </summary>
    private void MovePlayer()
    {
        // Slowly accelerate the player when giving movement input
        if (NBInputManager.Instance.MovementInput.magnitude > 0)
        {
            _targetVelocity = NBInputManager.Instance.MovementInput * configuration.MovementSpeed;

            _rigidBody.linearVelocity = Vector2.Lerp(_rigidBody.linearVelocity, _targetVelocity, configuration.MovementAcceletarion * Time.fixedDeltaTime);
        }
        // Deaccelerate the player when the movement input is not being pressed
        else
        {
            _rigidBody.linearVelocity = Vector2.Lerp(_rigidBody.linearVelocity, Vector2.zero, configuration.MovementDeaccelration * Time.fixedDeltaTime);
        }

        // Play particles
        if (NBInputManager.Instance.MovementInput != Vector2.zero) _playerController.PlayMovementParticles();
        else _playerController.StopMovementParticles();
    }

    /// <summary>
    /// Rotates the ship based on the aiming direction
    /// </summary>
    private void RotatePlayer()
    {
        if (AimInputInDeadZone()) return;

        Quaternion playerRotation;

        // To mantain the ship facing the last facing direction while using the gamepad when no aim input is being given
        if (NBInputManager.Instance.AimInput.x == 0 && NBInputManager.Instance.AimInput.y == 0)
        {
            Vector2 lastPoint = new Vector2(transform.position.x - _lastDirection.x * 0.5f, transform.position.y - _lastDirection.y * 0.5f);
            playerRotation = Quaternion.LookRotation(lastPoint, Vector3.forward);
        }
        else
        {
            Vector2 direction = Vector3.left * NBInputManager.Instance.AimInput.x + Vector3.down * NBInputManager.Instance.AimInput.y;
            playerRotation = Quaternion.LookRotation(direction, Vector3.forward);

            // Kep track of the last facing direction
            _lastDirection = direction;
        }

        _rigidBody.SetRotation(playerRotation);
    }

    /// <summary>
    /// Returns whether the given aim input is in the dead zone while using the gamepad
    /// </summary>
    /// <returns></returns>
    private bool AimInputInDeadZone()
    {
        return Mathf.Abs(NBInputManager.Instance.AimInput.x) < configuration.AimInputDeadZone && Mathf.Abs(NBInputManager.Instance.AimInput.y) < configuration.AimInputDeadZone && NBInputManager.Instance.IsGamepad;
    }
    #endregion
}
