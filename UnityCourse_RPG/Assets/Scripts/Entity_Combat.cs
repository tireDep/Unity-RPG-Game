using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public float damage = 10.0f;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        GetDetectedColliders();

        foreach(var target in GetDetectedColliders())
        {
            Entity_Health targetHP = target.GetComponent<Entity_Health>();
            targetHP?.TakeDamage(damage);

            // if (targetHP == null)
            //     continue;
            // 
            // targetHP.TakeDamage(10.0f);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
