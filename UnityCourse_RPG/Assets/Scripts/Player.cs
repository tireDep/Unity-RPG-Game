using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public Animator animator {  get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerInputSet input {  get; private set; }

    private StateMachine stateMachine;
    public Player_IdleState idleState {  get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }

    public Vector2 moveInput { get; private set; }

    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpForce;

    [Range(0,1)]
    public float inAirMoveMultiplier = 0.7f;
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = 0.7f;
    public bool facingRight = true;
    private int facingDir = 1;

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundDetected {  get; private set; }
    public bool wallDetected { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        input = new PlayerInputSet();

        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        moveState = new Player_MoveState(this, stateMachine, "Move");
        jumpState = new Player_JumpState(this, stateMachine, "JumpFall");
        fallState = new Player_FallState(this, stateMachine, "JumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "JumpFall");
    }

    private void OnEnable()
    {
        input.Enable();

        // input.Player.Movement.started : input just began
        // input.Player.Movement.performed : input is performed
        // input.Player.Movement.canceled : input stops, when you release the key

        // input.Player.Movement.performed += context => Debug.Log(context.ReadValue<Vector2>());  // 람다식
        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += context => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDectection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if( ( xVelocity > 0 && facingRight == false ) 
            || (xVelocity < 0 && facingRight == true) 
          )
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingRight = !facingRight;
        facingDir *= -1;
    }

    private void HandleCollisionDectection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * transform.right, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
        // Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * transform.right.x, 0, 0));
    }
}
