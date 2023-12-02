using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : Projectile
{
    
    protected override void Move()
    {
        transform.Translate(Vector3.forward * 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other. tag);
        
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
        

        Managers.Resource.Destroy(gameObject); 

        
        
        
        
    }
}
