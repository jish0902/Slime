using Server.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SoilBall : Projectile
{
<<<<<<< HEAD:Assets/Scripts/Gun/SoilBall.cs
 
=======

>>>>>>> parent of 7d17af6 (Bullet 재구성):Assets/Scripts/Gun/GroundBall.cs
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Wall")|| other.transform.CompareTag("Ground"))
        {
            Managers.Resource.Destroy(gameObject); 
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


            Managers.Resource.Destroy(gameObject); 
        }
    }
}
