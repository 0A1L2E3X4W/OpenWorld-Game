using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    [Header("Movement Settings")]
    [SerializeField] private float walkingSpeed = 1.5f;
    [SerializeField] private float runningSpeed = 4f;
    [SerializeField] private float rotationSpeed = 15f;

    // PLAYER
    private PlayerManager player;

    // MOVEMENT VALUES
    private float verticalMove;
    private float horizontalMove;
    private Vector3 moveDir;
    private Vector3 targetRotationDir;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotation();
    }

    private void GetVector2Inputs()
    {
        verticalMove = PlayerInputManager.Instance.verticalInput;
        horizontalMove = PlayerInputManager.Instance.horizontalInput;
    }

    private void HandleGroundedMovement()
    {
        GetVector2Inputs();

        moveDir = PlayerCamera.Instance.transform.forward * verticalMove;
        moveDir += (PlayerCamera.Instance.transform.right * horizontalMove);
        moveDir.Normalize();
        moveDir.y = 0f;

        if (PlayerInputManager.Instance.moveAmount > 0.5f)
        {
            player.characterController.Move(runningSpeed * Time.deltaTime * moveDir);
        }
        else if (PlayerInputManager.Instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(walkingSpeed * Time.deltaTime * moveDir);
        }
    }

    private void HandleRotation()
    {
        targetRotationDir = Vector3.zero;
        targetRotationDir = PlayerCamera.Instance.cameraObj.transform.forward * verticalMove;
        targetRotationDir += PlayerCamera.Instance.cameraObj.transform.right * horizontalMove;
        targetRotationDir.Normalize();
        targetRotationDir.y = 0f;

        if (targetRotationDir == Vector3.zero)
            targetRotationDir = transform.forward;

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
