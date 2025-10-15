using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    // PREFAB COMPONENTS
    private PlayerControls playerControls;

    [Header("Player Movement Inputs")]
    [SerializeField] private Vector2 movementInput;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public float moveAmount;

    [Header("Player Camera Inputs")]
    [SerializeField] private Vector2 cameraInput;
    [HideInInspector] public float cameraVerticalInput;
    [HideInInspector] public float cameraHorizontalInput;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChange;
        Instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene currentScene)
    {
        if (currentScene.buildIndex == WorldSaveGameManger.Instance.GetWorldIndex())
            Instance.enabled = true;
        else
            Instance.enabled = false;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus) { playerControls.Enable(); }
            else { playerControls.Disable(); }
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandleCameraInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
}