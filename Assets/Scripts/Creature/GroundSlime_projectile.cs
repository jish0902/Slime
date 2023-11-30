using Server.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class GroundSlime_projectile : MonoBehaviour
{
    public LivingEntity owner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Wall")|| other.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if(other.transform.CompareTag("Player"))
        {
            Debug.Log("Collided with: " + other.tag);
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


            Destroy(gameObject);
        }
    }
}
