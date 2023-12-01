using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : Projectile
{
    void Update()
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
                break;
            case Define.CreatureType.Monster:
                if (other.CompareTag("Wall"))
                {
                    
                }
                else if (other.CompareTag("Player"))
                {
                    dm = new DamageMessage() { amount = ((Monster)owner).monsterData.damage, damager = this.gameObject };
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
