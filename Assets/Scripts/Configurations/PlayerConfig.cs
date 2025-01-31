using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/Configurations/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float MovementAcceletarion { get; private set; }
    [field: SerializeField] public float MovementDeaccelration { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float AimInputDeadZone { get; private set; }

    // [Header("Combat")]
    // [field: SerializeField] public int Damage { get; private set; }
}
