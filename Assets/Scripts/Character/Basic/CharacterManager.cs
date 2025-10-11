using Unity.Netcode;
using UnityEngine;

public class CharacterManager : NetworkBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void Update()
    {

    }
}
