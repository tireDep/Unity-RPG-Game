using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public Collider2D[] targetColliders;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        GetDetectedColliders();

        foreach(var collider in targetColliders)
        {
            Debug.Log("Attacking : " + collider.name);
        }
    }

    private void GetDetectedColliders()
    {
        targetColliders = Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
