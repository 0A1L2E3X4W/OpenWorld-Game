using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkManager : NetworkBehaviour
{
    [Header("Network Position")]
    public Vector3 networkPosVelocity;
    public float networkPosSmoothTime = 0.1f;
    public NetworkVariable<Vector3> networkPos = 
        new(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Network Rotation")]
    public float networkRotationSmoothTime = 0.1f;
    public NetworkVariable<Quaternion> networkRotation = 
        new(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
}
