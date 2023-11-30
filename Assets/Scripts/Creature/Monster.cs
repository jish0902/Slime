
using Server.Data;
using UnityEngine;

public class Monster : LivingEntity
{
    public MonsterData monsterData;

    //시작시 반드시 실행해야할거
    protected override void OnEnable()
    {
        if(monsterData == null)
            Debug.LogError("data is null");
        
        dead = false;
        health = monsterData.hp;
    }

    public override void Die()
    {
        base.Die();
    }
}

