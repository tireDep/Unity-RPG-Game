using UnityEngine;

public class Enemy_Health : Entity_Health
{
    // 매번 가지고 오는 형태, 자주 사용할 경우 변수로 처리하는게 좋음
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, Transform damageDealer)
    {
        // if(damageDealer.CompareTag("Player") )
        // {
        //     enemy.TryEnterBattleState(damageDealer);
        // }

        // 위와 동일한 체크
        if (damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }
        
        base.TakeDamage(damage, damageDealer);
    }
}
