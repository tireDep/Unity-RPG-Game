using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] public float maxHP = 100;
    [SerializeField] protected bool isDead;

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

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
