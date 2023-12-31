using System.Collections;
using System.Collections.Generic;

using UnityEngine;

<<<<<<< HEAD:Assets/hoons/Script/FireBall.cs
public class FireBall : Projectile
=======
public class playerFire : MonoBehaviour
>>>>>>> parent of 7d17af6 (Bullet 재구성):Assets/hoons/Script/playerFire.cs
{
 
    protected override void Move()
    {
        Quaternion toRotate = Quaternion.LookRotation(Dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, speed * Time.deltaTime);

        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other. tag);
        
        DamageMessage dm;
        
        switch (owner.type)
        {
            case Define.CreatureType.Player:
                //플레이어가 쏠 때 
                if (other.CompareTag("Wall"))
                {
                    Managers.Resource.Destroy(gameObject);
                }
                else if (other.CompareTag("Monster"))
                {
                    //플레이어가 쏘고 몬스터가 맞았을때
                    dm = new DamageMessage() { amount = ((PlayerCharacter)owner).playerData.damage , damager = this.gameObject };
                    other.gameObject.GetComponent<Monster>().ApplyDamage(dm);
                }
                else if (other.transform.CompareTag("Torch"))
                {
                    Destroy(gameObject);
                    if (other.transform.GetComponent<Light>().intensity < 10)
                    {
                        other.transform.GetComponent<Light>().intensity += 5;
                    }
            
                }
                break;
            case Define.CreatureType.Monster:
                //몬스터가 쏠 떄
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
           
       
        }
        
    }
}
