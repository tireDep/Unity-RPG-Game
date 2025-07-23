using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat entityCombat;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    public void CurrentStateTrigger()
    {
        entity.SetCurrentStateAnimationTrigger();
    }

    private void SetAtttackTrigger()
    {
        entityCombat.PerformAttack();
    }
}
