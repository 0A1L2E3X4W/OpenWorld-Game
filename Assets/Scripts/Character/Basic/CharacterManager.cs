using Unity.Netcode;
using UnityEngine;

public class CharacterManager : NetworkBehaviour
{
    [Header("Prefab Components")]
    public CharacterController characterController;

    [Header("Manager Scripts")]
    public CharacterNetworkManager characterNetworkManager;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // INIT
        characterController = GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
    }

    protected virtual void Update()
    {
        if (IsOwner)
        {
            characterNetworkManager.networkPos.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        else
        {
            transform.SetPositionAndRotation(
                Vector3.SmoothDamp(
                    transform.position, 
                    characterNetworkManager.networkPos.Value, 
                    ref characterNetworkManager.networkPosVelocity,
                    characterNetworkManager.networkPosSmoothTime), 
                Quaternion.Slerp(
                    transform.rotation,
                    characterNetworkManager.networkRotation.Value,
                    characterNetworkManager.networkRotationSmoothTime));
        }
    }
}
