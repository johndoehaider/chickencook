using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float baseSpeed = 7f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float gravity = 25f;
    [SerializeField] private float jumpHeight = 2.2f;
    [SerializeField] private float dashDistance = 4f;
    [SerializeField] private float dashCooldown = 10f;
    [SerializeField] private float dashDuration = 0.2f;

    private CharacterController characterController;
    private float verticalVelocity;
    private bool isWalking;
    private bool isSprinting;
    private bool isJumping;
    private bool wasSprintingBeforeJump = false;
    private bool isDashing = false;
    private float dashTimeRemaining;
    private float dashCooldownRemaining;
    private Vector3 dashDirection;
    private Vector3 lastMoveDirection;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnDashAction += GameInput_OnDashAction;
    }

    private void OnDisable()
    {
        gameInput.OnJumpAction -= GameInput_OnJumpAction;
        gameInput.OnDashAction -= GameInput_OnDashAction;
    }

    private void Update()
    {
        bool sprintInput = gameInput.isSprintPressed();
        bool amIGrounded = characterController.isGrounded;
        if (amIGrounded)
        {
            wasSprintingBeforeJump = sprintInput;
        }

        isSprinting = amIGrounded ? sprintInput : wasSprintingBeforeJump;
        
        float currentSpeed = isSprinting ? baseSpeed * sprintMultiplier : baseSpeed;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        Vector3 horizontalVelocity;

        if (isDashing)
        {
            horizontalVelocity = dashDirection * dashDistance / dashDuration;
        }
        else
        {
            horizontalVelocity = moveDir * currentSpeed;
        }

        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f;
        }

        verticalVelocity -= gravity * Time.deltaTime;

        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;
        characterController.Move(velocity * Time.deltaTime);

        isJumping = !characterController.isGrounded;
        isWalking = moveDir != Vector3.zero;

        if (moveDir != Vector3.zero)
        {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
            lastMoveDirection = moveDir;  //used for dash direction
        }

        if (isDashing)
        {
            if (dashTimeRemaining > 0f)
            {
                dashTimeRemaining -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
            }
        }

        if (dashCooldownRemaining > 0f)
        {
            dashCooldownRemaining -= Time.deltaTime;
        }

    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2f);
    }

    private void GameInput_OnDashAction(object sender, System.EventArgs e)
    {
       if (dashCooldownRemaining <= 0f && isDashing == false && lastMoveDirection != Vector3.zero)
       {
           isDashing = true;
           dashTimeRemaining = dashDuration;
           dashCooldownRemaining = dashCooldown;
           dashDirection = lastMoveDirection.normalized;
       }
    }


    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsSprinting()
    {
        return isSprinting;
    }
    public bool IsJumping()
    {
        return isJumping;
    }

}

