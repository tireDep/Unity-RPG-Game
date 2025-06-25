using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public Animator animator {  get; private set; }
    public Rigidbody2D rb { get; private set; }

    private PlayerInputSet input;

    private StateMachine stateMachine;
    public Player_IdleState idleState {  get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Vector2 moveInput { get; private set; }

    [Header("Movement Details")]
    public float moveSpeed;

    public bool facingRight = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        input = new PlayerInputSet();

        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        moveState = new Player_MoveState(this, stateMachine, "Move");
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

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingRight = !facingRight;
    }
}
