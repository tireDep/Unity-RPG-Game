using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected StateMachine stateMachine;

    public bool facingRight = true;
    private int facingDir = 1;

    [Header("Collision Detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    [SerializeField] private Transform groundCheck;

    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    // Condition Variables
    private bool isKnocked;
    private Coroutine knockbackCorou;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDectection();
        stateMachine.UpdateActiveState();
    }

    public void SetCurrentStateAnimationTrigger()
    {
        stateMachine.currentState.SetAnimationTrigger();
    }

    public void RecieveKnockback(Vector2 knockback, float duration)
    {
        if (knockbackCorou != null)
            StopCoroutine(knockbackCorou);

        knockbackCorou = StartCoroutine(KnockbackCorou(knockback, duration));
    }

    private IEnumerator KnockbackCorou(Vector2 knockback, float duration)
    {
        isKnocked = true;
        rb.linearVelocity = knockback;

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked == true)
            return;

        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
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
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        if( secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * transform.right, wallCheckDistance, whatIsGround)
                       && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * transform.right, wallCheckDistance, whatIsGround);
        }
        else
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * transform.right, wallCheckDistance, whatIsGround);
        }

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * transform.right.x, 0, 0));
        
        if (secondaryWallCheck != null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * transform.right.x, 0, 0));
    }
}
