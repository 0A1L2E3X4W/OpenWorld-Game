using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    public Camera cameraObj;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
