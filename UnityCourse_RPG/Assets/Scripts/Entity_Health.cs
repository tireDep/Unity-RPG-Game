using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVFX;

    [SerializeField] public float maxHP = 100;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        entityVFX?.PlayOnDamageVfx();
        ReduceHP(damage);
    }

    protected void ReduceHP(float damage)
    {
        maxHP -= damage;

        if (maxHP <= 0)
            SetDie();
    }

    protected void SetDie()
    {
        isDead = true;
        Debug.Log("Entity Died!");
    }
}
