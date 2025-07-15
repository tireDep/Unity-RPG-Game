using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected StateMachine stateMachine;

    public bool facingRight = true;
    private int facingDir = 1;

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    private void Update()
    {
        HandleCollisionDectection();
        stateMachine.UpdateActiveState();
    }

    public void SetAnimationTrigger()
    {
        stateMachine.currentState.SetAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if ((xVelocity > 0 && facingRight == false)
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

        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * transform.right, wallCheckDistance, whatIsGround)
                       && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * transform.right, wallCheckDistance, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * transform.right.x, 0, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * transform.right.x, 0, 0));
    }
}
