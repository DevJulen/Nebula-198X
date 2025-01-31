using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerFeedback _playerFeedback;

    private void Awake()
    {
        Initialize();
    }

    #region PRIVATE METHODS
    /// <summary>
    /// Initializes the player controler components
    /// </summary>
    private void Initialize()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerFeedback = GetComponent<PlayerFeedback>();
    }
    #endregion

    #region PUBLIC FEEDBACK METHODS
    /// <summary>
    /// Plays the movement particles
    /// </summary>
    public void PlayMovementParticles()
    {
        _playerFeedback.PlayTrailParticles();
    }

    /// <summary>
    /// Stops the movement particles
    /// </summary>
    public void StopMovementParticles()
    {
        _playerFeedback.StopTrailParticles();
    }
    #endregion
}
