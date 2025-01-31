using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    private PlayerController _playerController;

    [Header("Movement Particles")]
    [SerializeField] private ParticleSystem _leftTrail;
    [SerializeField] private ParticleSystem _rightTrail;

    private void Awake()
    {
        Initialize();
    }

    #region PRIVATE METHODS
    /// <summary>
    /// Initializes the player feedback components
    /// </summary>
    private void Initialize()
    {
        _playerController = GetComponent<PlayerController>();
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Plays the trail particles
    /// </summary>
    public void PlayTrailParticles()
    {
        _leftTrail.Play();
        _rightTrail.Play();
    }

    /// <summary>
    /// Stops the trail particles
    /// </summary>
    public void StopTrailParticles()
    {
        _leftTrail.Stop();
        _rightTrail.Stop();
    }
    #endregion
}
