using Server.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class GroundBall : Projectile
{
    protected override void Move()
    {
        base.Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        DamageMessage dm;
        
        switch (owner.type)
        {
            case Define.CreatureType.Player:
                if (other.CompareTag("Wall"))
                {
                    Managers.Resource.Destroy(gameObject); 
                }
                else if (other.CompareTag("Monster"))
                {
                    //내가 플레이어고 상대가 몬스터 일 떄
                    dm = new DamageMessage() { amount = ((PlayerCharacter)owner).playerData.damage , damager = this.gameObject };
                    other.gameObject.GetComponent<Monster>().ApplyDamage(dm);
                }
                
                break;
            case Define.CreatureType.Monster:
                if (other.CompareTag("Wall"))
                {
                    Managers.Resource.Destroy(gameObject); 
                }
                else if (other.CompareTag("Player"))
                {
                    dm = new DamageMessage() { amount = ((Monster)owner).monsterData.Damage, damager = this.gameObject };
                    other.gameObject.GetComponent<PlayerCharacter>().ApplyDamage(dm);
                }
                break;
            case Define.CreatureType.Gimmick:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
       
    }
}
